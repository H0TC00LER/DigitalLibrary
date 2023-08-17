using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service.Implementations
{
    public class ImageLoaderService : IImageLoaderService
    {
        private readonly string _folderPath;

        public ImageLoaderService(IConfiguration configuration)
        {
            _folderPath = configuration["ImagesFolderPath"];
        }

        public async Task<byte[]?> GetPhotoAsync(Section section, string imageId)
        {
            string? imagePath = null;

            if (!File.Exists(imagePath))
                imagePath = GetPath(section, $"default.png");
            else
                imagePath = GetPath(section, $"{imageId}.png");

            var file = await File.ReadAllBytesAsync(imagePath);
            return file;
        }

        public async Task SavePhotoAsync(IFormFile file, Section section, string photoId)
        {
            var filePath = GetPath(section, $"{photoId}.png");

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }

        public void DeletePhoto(Section section, string photoId)
        {
            var filePath = GetPath(section, $"{photoId}.png");
            if (!File.Exists(filePath))
                throw new Exception($"There is no photo with id {photoId}");

            File.Delete(filePath);
        }

        public async Task ChangePhotoAsync(IFormFile file, Section section, string photoId)
        {
            var filePath = GetPath(section, $"{photoId}.png");
            if(!File.Exists(filePath))
                throw new Exception($"There is no photo with id {photoId}");

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }

        private string GetPath(Section section, string photoId)
        {
            var stringSection = section.ToString();
            var filePath = Path.Combine(_folderPath, stringSection, $"{photoId}.png");
            return filePath;
        }
    }
}
