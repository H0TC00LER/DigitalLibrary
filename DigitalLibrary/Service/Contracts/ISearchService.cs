using Domain.Entities;
using Domain.Enums;

namespace Service.Contracts
{
    public interface ISearchService
    {
        public IEnumerable<Book> Search(string searchWord, List<BookTag> bookTags, string sortBy, int page, int limit);
    }
}
