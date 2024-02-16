using LMS2.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS2.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data for Books
			modelBuilder.Entity<Book>().HasData(
				new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
				new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
				new Book { Id = 3, Title = "1984", Author = "George Orwell" }
			);

			// Seed data for Readers
			modelBuilder.Entity<Reader>().HasData(
				new Reader { ReaderId = 1, Name = "John Doe" },
				new Reader { ReaderId = 2, Name = "Jane Doe" },
				new Reader { ReaderId = 3, Name = "John Smith" }
			);

			// Seed data for Borrowings
			modelBuilder.Entity<Borrowing>().HasData(
				new Borrowing { Id = 1, ReaderId = 1, BookId = 1, BorrowDate = new DateTime(2021, 1, 1), ReturnDate = new DateTime(2021, 1, 15) },
				new Borrowing { Id = 2, ReaderId = 2, BookId = 2, BorrowDate = new DateTime(2021, 2, 1), ReturnDate = new DateTime(2021, 2, 15) },
				new Borrowing { Id = 3, ReaderId = 3, BookId = 3, BorrowDate = new DateTime(2021, 3, 1), ReturnDate = new DateTime(2021, 3, 15) }
			);
		}
	}
}
