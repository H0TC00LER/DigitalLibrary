using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects
{
    public class AuthorForRequestDto
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public IEnumerable<string>? WrittenBooksIds { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image {  get; set; }
    }
}
