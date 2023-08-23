using Domain.Entities;
using Domain.Enums;

namespace Service.Contracts
{
    public interface ISearchService
    {
        public Task<List<Book>> SearchAsync(string searchWord, List<BookTag> bookTags, string sortBy);
    }
}
