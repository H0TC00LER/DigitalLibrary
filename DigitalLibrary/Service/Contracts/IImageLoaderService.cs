using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Service.Contracts
{
    public interface IImageLoaderService
    {
        public Task<byte[]> GetPhotoAsync(Section sectionName, string imageId);
        public Task SavePhotoAsync(IFormFile file, Section section, string photoId);
        public void DeletePhoto(Section section, string photoId);
        public Task ChangePhotoAsync(IFormFile file, Section section, string photoId);
    }
}
