namespace Service.Contracts
{
    public interface IBookLoadingService
    {
        public Task<byte[]> LoadBook(string url);
        public string GetBookFilePath(string url);
    }
}
