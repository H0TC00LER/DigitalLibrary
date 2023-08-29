using Domain.DataTransferObjects.DtoForAnswer;
using Domain.DataTransferObjects.DtoForRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace DigitalLibrary.Controllers
{
    [Route("search")]
    public class SearchController
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService search) 
        {
            _searchService = search;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookForAnswerDto>> Search([FromBody] SearchQueryDto searchQuery)
        {
            var searchResult = _searchService.Search(
                searchQuery.SearchWord,
                searchQuery.BookTags,
                searchQuery.SortBy,
                searchQuery.Page,
                searchQuery.Limit);

            var searchResultDto = searchResult.Select(b => new BookForAnswerDto(b));

            return searchResultDto.ToList();
        }
    }
}
