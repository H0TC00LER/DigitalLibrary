namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Book> FavouriteBooks { get; set; }
        public string Description { get; set; }
    }
}
