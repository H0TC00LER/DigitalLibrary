namespace Domain.Entities
{
    public class Author
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public IEnumerable<Book>? WrittenBooks { get; set; }
        public string? PhotoId { get; set; }
    }
}
