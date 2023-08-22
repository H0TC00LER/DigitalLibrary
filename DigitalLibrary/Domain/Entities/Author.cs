using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Author
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<Book> WrittenBooks { get; set; } = new List<Book>();
        public string? PhotoId { get; set; }
    }
}
