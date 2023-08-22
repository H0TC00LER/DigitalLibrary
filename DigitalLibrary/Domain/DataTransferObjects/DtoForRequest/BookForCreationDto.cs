using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.DtoForRequest
{
    public class BookForCreationDto
    {
        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? CoverImage { get; set; }

        public string AuthorId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile PdfText { get; set; }

        public List<BookTag>? BookTags { get; set; }
    }
}
