using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Implementations
{
    public class BookLoadingService : IBookLoadingService
    {
        private readonly string _booksFolderPath;

        public BookLoadingService(IConfiguration configuration)
        {
            _booksFolderPath = configuration["BookContentPath"];
        }

        public async Task<byte[]?> LoadBookAsync(string id)
        {
            var bookPath = GetBookFilePath(id);
            if (!File.Exists(bookPath))
                return null;

            var book = await File.ReadAllBytesAsync(bookPath);
            return book;
        }

        public string GetBookFilePath(string id)
        {
            var fileName = $"{id}.pdf";
            var bookFilePath = Path.Combine(_booksFolderPath, fileName);

            return bookFilePath;
        }

        public async Task SaveBookAsync(IFormFile file, string textId)
        {
            var filePath = GetBookFilePath(textId);

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }

        public void DeleteBook(string textId)
        {
            var filePath = GetBookFilePath(textId);
            if(!File.Exists(filePath))
                throw new Exception($"There is no book with id {textId}");

            File.Delete(filePath);
        }

        public async Task ChangeBookAsync(IFormFile file, string textId)
        {
            if (file == null)
                throw new Exception("File was null.");

            var filePath = GetBookFilePath(textId);
            if (!File.Exists(filePath))
                throw new Exception($"There is no book with id {textId}");

            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        }
    }
}
