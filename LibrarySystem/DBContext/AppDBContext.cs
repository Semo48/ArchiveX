using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.DBContext
  
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BorrowProcess> BorrowProcess { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            // User-Book Reviews (One-to-Many: One User can make many reviews)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            // Book-Review Relationship (One-to-Many: One Book can have many Reviews)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);

            // Book-Category Relationship (Many-to-One: Many Books belong to one Category)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            // Book-Publisher Relationship (Many-to-One: Many Books can have one Publisher)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            // BorrowProcess-User Relationship (Many-to-One: A BorrowProcess involves one User)
            modelBuilder.Entity<BorrowProcess>()
                .HasOne(bp => bp.User)
                .WithMany(u => u.BorrowingProcess)
                .HasForeignKey(bp => bp.UserId);

            // BorrowProcess-Book Relationship (Many-to-One: A BorrowProcess involves one Book)
            modelBuilder.Entity<BorrowProcess>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BorrowingTransactions)
                .HasForeignKey(bp => bp.BookId);

            // Ensuring unique Email for Users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Seeding initial data for categories (optional)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Science Fiction" },
                new Category { CategoryId = 2, CategoryName = "History" },
                new Category { CategoryId = 3, CategoryName = "Romance" }
            );

            // Seeding initial data for publishers (optional)
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { PublisherId = 1, PublisherName = "Penguin Random House" },
                new Publisher { PublisherId = 2, PublisherName = "HarperCollins" }
            );
        }

    }
}
