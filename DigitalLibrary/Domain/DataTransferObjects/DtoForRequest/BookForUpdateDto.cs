using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.DtoForRequest
{
    public class BookForUpdateDto
    {
        [Required]
        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile CoverImage { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? PdfText { get; set; }

        public List<BookTag>? BookTags { get; set; }
    }
}
