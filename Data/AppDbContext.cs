namespace Book_API.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, FirstName = "John", LastName = "Doe" },
            new Author { Id = 2, FirstName = "Paul", LastName = "Smith" }
        );
        
        modelBuilder.Entity<Book>()
            .HasOne(book => book.Author)
            .WithMany()
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
