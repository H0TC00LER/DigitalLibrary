using Domain.Entities;
using Domain.Enums;
using System;
namespace Domain.DataTransferObjects
{
    public class BookForAnswerDto
    {
        public BookForAnswerDto() { }

        public BookForAnswerDto(Book book)
        {
            Id = book.Id;
            AuthorId = book.Author == null ? null : book.Author.Id;
            BookTags = book.BookTags;
            CoverUrl = book.CoverUrl;
            Description = book.Description;
            PublicationDate = book.PublicationDate;
            TextId = book.TextId;
            Title = book.Title;
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Description { get; set; }
        public string? CoverUrl { get; set; }
        public string? AuthorId { get; set; }
        public string TextId { get; set; }
        public List<BookTag>? BookTags { get; set; }
    }
}
