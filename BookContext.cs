using Microsoft.EntityFrameworkCore;
using Models;

namespace BookLibraryAPI.Data
{
	
	public class BookContext : DbContext
	{
		
		public BookContext(DbContextOptions<BookContext> options) : base(options) { }
		public DbSet<Ebook> Ebooks { get; set; }
		public DbSet<PaperBook> PaperBooks { get; set; }


	}
}
