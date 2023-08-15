using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Implementations
{
    public class ImageLoaderService : IImageLoaderService
    {
        private readonly string _folderPath;

        public ImageLoaderService(IConfiguration configuration)
        {
            _folderPath = configuration["ImagesFolderPath"];
        }

        public async Task<byte[]> GetAuthorPhotoAsync(string photoId)
            => await GetPhotoAsync("AuthorPhotos", photoId);

        public async Task<byte[]> GetCoverAsync(string coverId)
            => await GetPhotoAsync("Covers", coverId);

        public async Task<byte[]> GetUserPhotoAsync(string photoId)
            => await GetPhotoAsync("UserPhotos", photoId);

        public async Task<byte[]?> GetPhotoAsync(string sectionName, string imageId)
        {
            string? imagePath = null;

            if (!File.Exists(imagePath))
                imagePath = Path.Combine(_folderPath, sectionName, $"default.png");
            else
                imagePath = Path.Combine(_folderPath, sectionName, $"{imageId}.png");

            var file = await File.ReadAllBytesAsync(imagePath);
            return file;
        }

        public async Task SavePhotoAsync(IFormFile file, string section, string photoId)
        {
            var filePath = Path.Combine(_folderPath, section, $"{photoId}.png");

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }
    }
}
