namespace Service.Contracts
{
    public interface IImageLoaderService
    {
        public Task<byte[]> GetCoverAsync(string photoId);
        public Task<byte[]> GetAuthorPhotoAsync(string coverId);
        public Task<byte[]> GetUserPhotoAsync(string photoId);
        public Task<byte[]> GetPhotoAsync(string sectionName, string imageId);
    }
}
