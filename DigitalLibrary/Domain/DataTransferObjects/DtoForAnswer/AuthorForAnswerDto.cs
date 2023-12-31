﻿using Domain.Entities;

namespace Domain.DataTransferObjects.DtoForAnswer
{
    public class AuthorForAnswerDto
    {
        public AuthorForAnswerDto(Author author)
        {
            Id = author.Id;
            Description = author.Description;
            FirstName = author.FirstName;
            LastName = author.LastName;
            PhotoId = author.PhotoId;
            WrittenBooksIds = author.WrittenBooks.Select(b => b.Id);
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public IEnumerable<string>? WrittenBooksIds { get; set; }
        public string? PhotoId { get; set; }
    }
}
