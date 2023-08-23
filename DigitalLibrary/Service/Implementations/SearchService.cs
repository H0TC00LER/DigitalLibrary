using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Service.Contracts;

namespace Service.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly AppDbContext _context;

        public SearchService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> Search(string searchWord,
            List<BookTag> bookTags,
            string sortBy,
            int page = 1,
            int limit = 30)
        {
            var books = _context.Books.Include(b => b.Author).AsNoTracking().AsEnumerable();
            books = FilterByTags(books, bookTags);
            books = FilterBySearchWord(books, searchWord);
            books = Sort(books, sortBy);
            books = LimitByPage(books, page, limit);

            return books;
        }

        private static IEnumerable<Book> FilterByTags(IEnumerable<Book> books, List<BookTag> bookTags) 
            => books.Where(b => !bookTags.Except(b.BookTags).Any());

        private static IEnumerable<Book> FilterBySearchWord(IEnumerable<Book> books, string searchWord)
        {
            return books.Where
                (
                b => ContainsIgnoreCase(b.Title, searchWord)
                    || ContainsIgnoreCase(b.Author.FirstName, searchWord)
                    || ContainsIgnoreCase(b.Author.LastName, searchWord)
                );
        }

        private static bool ContainsIgnoreCase(string firstWord, string secondWord)
            => firstWord.Contains(secondWord, StringComparison.OrdinalIgnoreCase);

        private static IEnumerable<Book> Sort(IEnumerable<Book> books, string sortBy)
        {
            if (sortBy == null)
                return books;

            sortBy = sortBy.ToLower();
            switch(sortBy)
            {
                case "title":
                    return books.OrderBy(b => b.Title);
                case "date":
                    return books.OrderBy(b => b.PublicationDate);
                case "author":
                    return books.OrderBy(b => b.Author.FirstName);
                default:
                    return books;
            }
        }

        private static IEnumerable<Book> LimitByPage(IEnumerable<Book> books, int page, int limit)
            => books.Skip((page - 1) * limit).Take(limit);
    }
}
