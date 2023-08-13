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

        public async Task<byte[]> GetAuthorPhotoAsync(string photoId)
            => await GetPhotoAsync("AuthorPhotos", photoId);

        public async Task<byte[]> GetCoverAsync(string coverId)
            => await GetPhotoAsync("Covers", coverId);

        public async Task<byte[]> GetUserPhotoAsync(string photoId)
            => await GetPhotoAsync("UserPhotos", photoId);

        public async Task<byte[]?> GetPhotoAsync(string sectionName, string imageId)
        {
            var imagePath = Path.Combine(_folderPath, sectionName, $"{imageId}.png");
            if (!File.Exists(imagePath))
                return null;

            var file = await File.ReadAllBytesAsync(imagePath);
            return file;
        }
    }
}
