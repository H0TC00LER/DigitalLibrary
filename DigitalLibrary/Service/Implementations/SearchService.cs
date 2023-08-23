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

        public async Task<List<Book>> SearchAsync(string searchWord, List<BookTag> bookTags, string sortBy)
        {
            var books = _context.Books.AsNoTracking();
            books = FilterByTags(books, bookTags);
            books = FilterBySearchWord(books, searchWord);
            books = Sort(books, sortBy);

            return await books.ToListAsync();
        }

        private static IQueryable<Book> FilterByTags(IQueryable<Book> books, List<BookTag> bookTags) 
            => books.Where(b => bookTags.Except(b.BookTags).Any());

        private static IQueryable<Book> FilterBySearchWord(IQueryable<Book> books, string searchWord)
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

        private static IQueryable<Book> Sort(IQueryable<Book> books, string sortBy)
        {
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
    }
}
