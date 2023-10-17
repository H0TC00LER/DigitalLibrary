using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Net;

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

        [HttpGet("{section}/{imageId}")]
        public async Task<IActionResult> GetImage(string section, string imageId)
        {
            var isParseSucceed = Enum.TryParse(typeof(Section), section, true, out var parsedSection);
            if (!isParseSucceed)
                return NotFound($"There is no section named {section}.");

            var image = await _imageService.GetPhotoAsync((Section) parsedSection!, imageId);

            Response.ContentType = "image/png";
            await Response.Body.WriteAsync(image, 0, image.Length);

            Response.StatusCode = 200;
            return Ok();
        }
    }
}
