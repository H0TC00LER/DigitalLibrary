using DigitalLibrary.Filters;
using Domain.DataTransferObjects;
using Domain.Entities;
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
            var author = await _context.Authors.FindAsync(book.AuthorId);
            if (author == null)
                return NotFound($"There is no author with id {book.AuthorId}");

            Book? bookModel = new Book
            {
                Title = book.Title,
                BookTags = book.BookTags,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                Author = author,
            };

            var textId = Guid.NewGuid().ToString();
            await _bookLoadingService.SaveBookAsync(book.PdfText, textId);
            bookModel.TextId = textId;

            var imageId = Guid.NewGuid().ToString();
            await _imageLoaderService.SavePhotoAsync(book.CoverImage, "AuthorPhotos", imageId);
            bookModel.CoverUrl = imageId;

            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
