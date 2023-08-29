using DigitalLibrary.Filters;
using Domain.DataTransferObjects.DtoForAnswer;
using Domain.DataTransferObjects.DtoForRequest;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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
                .Include(a => a.WrittenBooks)
                .Select(a => new AuthorForAnswerDto(a));

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

            var authorDto = new AuthorForAnswerDto(author);

            return authorDto;
        }

        [HttpPost]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> PostAuthor([FromBody] AuthorForCreationDto authorDto)
        {
            var authorId = Guid.NewGuid().ToString();

            var author = new Author
            {
                Description = authorDto.Description,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
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
        [Authorize]
        public async Task<IActionResult> UpdateAuthor(string id, [FromBody] AuthorForCreationDto author)
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
        [Authorize]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var authorToDelete = await _context.Authors.FindAsync(id);
            if (authorToDelete == null)
                return NotFound($"There is no author with id {id}.");

            if(authorToDelete.PhotoId != null)
                _imageService.DeletePhoto(Section.AuthorPhotos, authorToDelete.PhotoId);

            _context.Authors.Remove(authorToDelete);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
