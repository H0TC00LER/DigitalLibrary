using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Contracts;

namespace DigitalLibrary.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookLoadingService _bookLoadingService;
        public BooksController(AppDbContext context, IBookLoadingService bookLoadingService)
        {
            _context = context;
            _bookLoadingService = bookLoadingService;
        }

        [HttpGet("test")]
        public ActionResult<string> DoTest()
        {
            Console.WriteLine(_context.Users.First().Id);
            return _context.Users.First().Id;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _context.Books.AsNoTracking().ToList();
            return books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _context.Books.AsNoTracking().SingleOrDefault(b => b.Id == id);
            if(book == null)
                return NotFound($"There's no book with id {id}");

            return book;
        }

        [HttpGet("{id}/content")]
        public async Task<ActionResult> GetBookContent(string url)
        {
            var byteBook = await _bookLoadingService.LoadBook(url);
            Response.Headers.Add("Content-Disposition", $"inline; filename={url}.pdf");
            Response.ContentType = "application/pdf";
            await Response.Body.WriteAsync(byteBook, 0, byteBook.Length);

            return Ok();
        }
    }
}
