using DigitalLibrary.Filters;
using Domain.DataTransferObjects.DtoForRequest;
using Domain.DataTransferObjects.DtoForAnswer;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Contracts;
using Persistance.Extencions;
using Microsoft.AspNetCore.Authorization;

namespace DigitalLibrary.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookLoadingService _bookService;
        private readonly IImageLoaderService _imageService;
        private readonly ISearchService _searchService;

        public BooksController(AppDbContext context,
            IBookLoadingService bookService,
            IImageLoaderService imageService,
            ISearchService searchService)
        {
            _context = context;
            _bookService = bookService;
            _imageService = imageService;
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookForAnswerDto>>> GetAllBooks()
        {
            var bookModels = await _context
                .Books
                .AsNoTracking()
                .Select(b => new BookForAnswerDto(b))
                .ToListAsync();

            return bookModels;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookForAnswerDto>> GetBookById(string id)
        {
            var bookModel = await _context
                .Books
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id);

            if (bookModel == null)
                return NotFound($"There's no book with id {id}.");

            var bookDto = new BookForAnswerDto(bookModel);

            return bookDto;
        }

        [HttpGet("texts/{textId}")]
        [Authorize]
        public async Task<ActionResult> GetBookText(string textId)
        {
            var byteBook = await _bookService.LoadBookAsync(textId);

            if (byteBook == null)
                return NotFound($"There is no book with id {textId}.");

            Response.Headers.Add("Content-Disposition", $"inline; filename={textId}.pdf");
            Response.ContentType = "application/pdf";
            await Response.Body.WriteAsync(byteBook, 0, byteBook.Length);

            return Ok();
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto book)
        {
            var author = await _context.Authors.Include(a => a.WrittenBooks).SingleOrDefaultAsync(a => a.Id == book.AuthorId);
            if (author == null)
                return NotFound($"There is no author with id {book.AuthorId}");

            var bookId = Guid.NewGuid().ToString();
            var bookModel = new Book
            {
                Id = bookId,
                Title = book.Title,
                BookTags = book.BookTags,
                Description = book.Description,
                PublicationDate = book.PublicationDate,
                Author = author
            };

            var textId = Guid.NewGuid().ToString();
            await _bookService.SaveBookAsync(book.PdfText, textId);
            bookModel.TextId = textId;

            if(book.CoverImage != null)
            {
                var imageId = Guid.NewGuid().ToString();
                await _imageService.SavePhotoAsync(book.CoverImage, Section.Covers, imageId);
                bookModel.CoverId = imageId;
            }    

            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateBook), new BookForAnswerDto(bookModel));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var bookToDelete = await _context.Books.FindAsync(id);
            if (bookToDelete == null)
                return NotFound($"There is no book with id {id}.");

            _imageService.DeletePhoto(Section.Covers, bookToDelete.CoverId);
            _bookService.DeleteBook(bookToDelete.TextId);

            _context.Books.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] BookForUpdateDto book)
        {
            var bookToChange = await _context.Books.FindAsync(id);
            if (bookToChange == null)
                return NotFound($"There is no book with id {id}.");

            if (book.CoverImage != null)
            {
                if (bookToChange.CoverId == null)
                {
                    var imageId = Guid.NewGuid().ToString();
                    await _imageService.SavePhotoAsync(book.CoverImage, Section.Covers, imageId);
                    bookToChange.CoverId = imageId;
                }
                else
                    await _imageService.ChangePhotoAsync(book.CoverImage, Section.Covers, bookToChange.CoverId);
            }

            if(book.PdfText != null)
            {
                await _bookService.ChangeBookAsync(book.PdfText, bookToChange.TextId);
            }

            await _context.UpdateBookAsync(id, book);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
