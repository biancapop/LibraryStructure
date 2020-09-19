using Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.DBRepositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Editorial> Editorials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Editorial>()
                .HasMany(c => c.Categories)
                .WithOne(e => e.Editorial);

            modelBuilder.Entity<Editorial>()
               .HasMany(c => c.Books)
               .WithOne(e => e.Editorial);

            modelBuilder.Entity<Book>()
            .HasOne(s => s.Category)
            .WithMany(g => g.Books)
            .HasForeignKey(s => s.IdCategory).IsRequired(false);
        }
    }
}