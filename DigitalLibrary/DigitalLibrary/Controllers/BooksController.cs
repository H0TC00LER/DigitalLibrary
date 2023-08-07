using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace DigitalLibrary.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet("")]
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
    }
}
