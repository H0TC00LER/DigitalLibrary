using DigitalLibrary.Filters;
using Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Service.Contracts;

namespace DigitalLibrary.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookLoadingService _bookLoadingService;
        private readonly IImageLoaderService _imageLoaderService;

        public BooksController(AppDbContext context, IBookLoadingService bookLoadingService, IImageLoaderService imageLoaderService)
        {
            _context = context;
            _bookLoadingService = bookLoadingService;
            _imageLoaderService = imageLoaderService;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Book>> GetAllBooks()
        //{
        //    var books = _context.Books.AsNoTracking().ToList();
        //    return books;
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Book> GetBook(string id)
        //{
        //    var book = _context.Books.AsNoTracking().SingleOrDefault(b => b.Id == id);
        //    if(book == null)
        //        return NotFound($"There's no book with id {id}");

        //    return book;
        //}

        [HttpGet("texts/{textId}")]
        public async Task<ActionResult> GetBookText(string textId)
        {
            var byteBook = await _bookLoadingService.LoadBookAsync(textId);

            if (byteBook == null)
                return NotFound($"There is no book with id {textId}.");

            Response.Headers.Add("Content-Disposition", $"inline; filename={textId}.pdf");
            Response.ContentType = "application/pdf";
            await Response.Body.WriteAsync(byteBook, 0, byteBook.Length);

            return Ok();
        }

        [HttpPost("texts/save")]
        [ValidateModel]
        public async Task<IActionResult> CreateBook(BookForCreationDto book)
        {
            var textId = Guid.NewGuid().ToString();
            await _bookLoadingService.SaveBookAsync(book.PdfText, textId);
            //Сохранить в дб айдишники!!!!!!!!!!!!!!!
            //Отрефакторить сохранятель картинок!!!!!!

            var imageId = Guid.NewGuid().ToString();
            await _imageLoaderService.SavePhotoAsync(book.CoverImage, "AuthorPhotos", imageId);


            return Ok();
        }
    }
}
