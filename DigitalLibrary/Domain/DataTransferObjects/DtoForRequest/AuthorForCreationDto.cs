using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.DtoForRequest
{
    public class AuthorForCreationDto
    {
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? Image { get; set; }
    }
}
