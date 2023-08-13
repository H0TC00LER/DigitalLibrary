using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace DigitalLibrary.Controllers
{
    [Route("images")]
    public class ImagesController : Controller
    {
        private readonly IImageLoaderService _imageService;

        public ImagesController(IImageLoaderService imageService)
        {
            _imageService = imageService;
        }

        //DRY умер
        [HttpGet("covers/{coverId}")]
        public async Task<IActionResult> GetCover(string coverId)
        {
            var cover = await _imageService.GetCoverAsync(coverId);

            if(cover == null)
                return NotFound($"There is no cover with id {coverId}.");

            Response.ContentType = "image/png";
            await Response.Body.WriteAsync(cover, 0, cover.Length);

            return Ok();
        }

        [HttpGet("userPhotos/{photoId}")]
        public async Task<IActionResult> GetUserPhoto(string photoId)
        {
            var photo = await _imageService.GetUserPhotoAsync(photoId);

            if (photo == null)
                return NotFound($"There is no user photo with id {photoId}.");

            Response.ContentType = "image/png";
            await Response.Body.WriteAsync(photo, 0, photo.Length);

            return Ok();
        }

        [HttpGet("authorPhotos/{photoId}")]
        public async Task<IActionResult> GetAuthorPhoto(string photoId)
        {
            var photo = await _imageService.GetUserPhotoAsync(photoId);

            if (photo == null)
                return NotFound($"There is no author photo with id {photoId}.");

            Response.ContentType = "image/png";
            await Response.Body.WriteAsync(photo, 0, photo.Length);

            return Ok();
        }
    }
}
