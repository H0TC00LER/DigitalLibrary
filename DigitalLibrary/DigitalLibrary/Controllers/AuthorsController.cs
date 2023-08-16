using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Contracts;

namespace DigitalLibrary.Controllers
{
    [Route("authors")]
    public class AuthorsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IImageLoaderService _imageService;

        public AuthorsController(AppDbContext context, IImageLoaderService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAllAuthors()
        {
            var authors = _context
                .Authors
                .AsNoTracking()
                .Select(a => new AuthorDto
            {
                Description = a.Description,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhotoId = a.PhotoId,
                WrittenBooksIds = a.WrittenBooks.Select(b => b.Id)
            });

            return await authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(string id)
        {
            var author = await _context
                .Authors
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);

            if(author == null)
                return NotFound($"There is no author with id {id}");

            var authorDto = new AuthorDto
            {
                Description = author.Description,
                FirstName = author.FirstName,
                LastName = author.LastName,
                PhotoId = author.PhotoId,
                WrittenBooksIds = author.WrittenBooks.Select(b => b.Id)
            };

            return authorDto;
        }

        [HttpPost]
        public async Task<IActionResult> PostAuthor(AuthorForRequestDto authorDto)
        {
            var writtenBooks = await _context
                .Books
                .Where(b => authorDto.WrittenBooksIds.Contains(b.Id))
                .ToListAsync();

            var author = new Author
            {
                Description = authorDto.Description,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                WrittenBooks = writtenBooks,
            };

            if(authorDto.Image != null)
            {
                var imageId = Guid.NewGuid().ToString();
                await _imageService.SavePhotoAsync(authorDto.Image, "AuthorPhotos", imageId);

                author.PhotoId = imageId;
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostAuthor), author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeAuthor(string id, AuthorDto author)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthot(string id)
        {
            return Ok();
        }
    }
}
