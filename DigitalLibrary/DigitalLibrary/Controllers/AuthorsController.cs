using DigitalLibrary.Filters;
using Domain.DataTransferObjects;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Extencions;
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
        public async Task<ActionResult<List<AuthorForAnswerDto>>> GetAllAuthors()
        {
            var authors = _context
                .Authors
                .AsNoTracking()
                .Select(a => new AuthorForAnswerDto
            {
                Id = a.Id,
                Description = a.Description,
                FirstName = a.FirstName,
                LastName = a.LastName,
                PhotoId = a.PhotoId,
                WrittenBooksIds = a.WrittenBooks.Select(b => b.Id)
            });

            return await authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorForAnswerDto>> GetAuthor(string id)
        {
            var author = await _context
                .Authors
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id);

            if(author == null)
                return NotFound($"There is no author with id {id}");

            var authorDto = new AuthorForAnswerDto
            {
                Id = author.Id,
                Description = author.Description,
                FirstName = author.FirstName,
                LastName = author.LastName,
                PhotoId = author.PhotoId,
                WrittenBooksIds = author.WrittenBooks == null ? null : author.WrittenBooks.Select(b => b.Id)
            };

            return authorDto;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> PostAuthor(AuthorForRequestDto authorDto)
        {
            var writtenBooks = await _context
                .Books
                .Where(b => authorDto.WrittenBooksIds != null && authorDto.WrittenBooksIds.Contains(b.Id))
                .ToListAsync();

            var authorId = Guid.NewGuid().ToString();

            var author = new Author
            {
                Description = authorDto.Description,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                WrittenBooks = writtenBooks,
                Id = authorId,
            };

            if(authorDto.Image != null)
            {
                var imageId = Guid.NewGuid().ToString();
                await _imageService.SavePhotoAsync(authorDto.Image, Section.AuthorPhotos, imageId);

                author.PhotoId = imageId;
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostAuthor), author);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAuthor(string id, AuthorForRequestDto author)
        {
            var authorToChange = await _context.Authors.FindAsync(id);
            if (authorToChange == null)
                return NotFound($"There is no author with id {id}.");

            if(author.Image != null)
            {
                if (authorToChange.PhotoId == null)
                {
                    var imageId = Guid.NewGuid().ToString();
                    await _imageService.SavePhotoAsync(author.Image, Section.AuthorPhotos, imageId);
                    authorToChange.PhotoId = imageId;
                }
                else
                    await _imageService.ChangePhotoAsync(author.Image, Section.AuthorPhotos, authorToChange.PhotoId);
            }

            await _context.UpdateAuthorAsync(id, author);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var authorToDelete = await _context.Authors.FindAsync(id);
            if (authorToDelete == null)
                return NotFound($"There is no author with id {id}.");

            _imageService.DeletePhoto(Section.AuthorPhotos, authorToDelete.PhotoId);

            _context.Authors.Remove(authorToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
