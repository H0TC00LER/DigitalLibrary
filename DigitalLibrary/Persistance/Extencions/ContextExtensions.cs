using Domain.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Extencions
{
    static public class ContextExtensions
    {
        public static async Task UpdateAuthorAsync(this AppDbContext context, string id, AuthorForRequestDto author)
        {
            var authorToUpdate = await context.Authors.FindAsync(id);
            if (authorToUpdate == null)
                throw new Exception($"There is no author with id {id}.");

            var writtenBooks = await context
                .Books
                .Where(b => author.WrittenBooksIds != null && author.WrittenBooksIds.Contains(b.Id))
                .ToListAsync();

            authorToUpdate.Description = author.Description;
            authorToUpdate.WrittenBooks = writtenBooks;
            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.LastName = author.LastName;
        }
    }
}
