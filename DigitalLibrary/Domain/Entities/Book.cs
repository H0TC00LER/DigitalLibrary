using Domain.Enums;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public Author Author { get; set; }
        public string TextUrl { get; set; }
        public BookTag BookTag { get; set; }
    }
}
