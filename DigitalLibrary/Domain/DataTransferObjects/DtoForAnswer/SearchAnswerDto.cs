namespace Domain.DataTransferObjects.DtoForAnswer
{
    public class SearchAnswerDto
    {
        public SearchAnswerDto(List<BookForAnswerDto> books, int booksTotalAmount)
        {
            Books = books;
            BooksTotalAmount = booksTotalAmount;
        }
    
        public List<BookForAnswerDto> Books { get; set; }
        public int BooksTotalAmount { get; set; }
    }
}
