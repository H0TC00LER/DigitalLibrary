using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service.Implementations
{
    public class BookLoadingService : IBookLoadingService
    {
        private readonly string _booksFolderPath;

        public BookLoadingService(IConfiguration configuration)
        {
            _booksFolderPath = configuration["BookContentPath"];
        }

        public async Task<byte[]> LoadBook(string url)
        {
            var book = await File.ReadAllBytesAsync(GetBookFilePath(url));
            return book;
        }

        public string GetBookFilePath(string url)
        {
            var fileName = $"{url}.pdf";
            var bookFilePath = Path.Combine(_booksFolderPath, fileName);

            return bookFilePath;
        }
    }
}
