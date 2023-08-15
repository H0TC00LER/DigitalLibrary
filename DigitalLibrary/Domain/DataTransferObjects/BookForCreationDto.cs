using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "Type the book title.")]
        public string Title { get; set; }

        public DateTime? PublicationDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile CoverImage { get; set; }

        [Required(ErrorMessage = "AuthorId is required.")]
        public string AuthorId { get; set; }

        [Required(ErrorMessage = "Book text is required.")]
        [DataType(DataType.Upload)]
        public IFormFile PdfText { get; set; }

        public List<BookTag>? BookTags { get; set; }
    }
}
