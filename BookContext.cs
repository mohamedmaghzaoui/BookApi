using Microsoft.EntityFrameworkCore;
using Models;

namespace BookLibrary.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public DbSet<Media> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .HasDiscriminator<string>("Type")
                .HasValue<Ebook>("Ebook")
                .HasValue<PaperBook>("PaperBook");
        }

    }
}
