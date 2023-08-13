using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistance
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //нереальный кринж
            modelBuilder.Entity<Book>()
                .Property(b => b.BookTags)
                .HasConversion
                (
                    v => string.Join(",", v.Select(t => t.ToString())),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(tag => Enum.Parse<BookTag>(tag)).ToList(),
                    new ValueComparer<List<BookTag>>
                    (
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList()
                    )
                );
        }


        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
