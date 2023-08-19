using Domain.DataTransferObjects.DtoForRequest;
using Domain.Entities;
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

        public static async Task UpdateBookAsync(this AppDbContext context, string id, BookForUpdateDto book)
        {
            var bookToUpdate = await context.Books.FindAsync(id);
            if(bookToUpdate == null)
                throw new Exception($"There is no book with id {id}.");

            var author = await context.Authors.FindAsync(book.AuthorId);

            bookToUpdate.Author = author;
            bookToUpdate.Description = book.Description;
            bookToUpdate.PublicationDate = book.PublicationDate;
            bookToUpdate.BookTags = book.BookTags;
            bookToUpdate.Title = book.Title;
        }
    }
}
