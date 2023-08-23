using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.DataTransferObjects.DtoForRequest
{
    public class SearchQueryDto
    {
        [MaxLength(100)]
        public string SearchWord { get; set; } = string.Empty;

        public List<BookTag> BookTags { get; set; } = new List<BookTag>();

        public string SortBy { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(0, int.MaxValue)]
        public int Limit { get; set; } = 30;
    }
}
