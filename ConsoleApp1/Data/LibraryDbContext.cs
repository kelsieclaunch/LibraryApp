using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Models;

namespace PersonalLibrary.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookReading> BookReadings => Set<BookReading>();
        public DbSet<ReadingStatus> ReadingStatuses => Set<ReadingStatus>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureBook(modelBuilder);
            ConfigureBookReading(modelBuilder);
            ConfigureReadingStatus(modelBuilder);
        }

        private static void ConfigureBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.ISBN).HasMaxLength(20);
                entity.HasIndex(e => e.ISBN).IsUnique().HasFilter("[ISBN] IS NOT NULL");
            });
        }

        private static void ConfigureBookReading(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookReading>(entity =>
            {
                entity.HasKey(e => e.BookReadingId);
                entity.Property(e => e.DateAdded).IsRequired();
                entity.Property(e => e.ReadingStatusId).HasDefaultValue(1); //automatically sets to TBR
                entity.HasOne(e => e.Book)
                      .WithMany(b => b.BookReadings)
                      .HasForeignKey(e => e.BookId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ReadingStatus)
                      .WithMany(rs => rs.BookReadings)
                      .HasForeignKey(e => e.ReadingStatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void ConfigureReadingStatus(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReadingStatus>(entity =>
            {
                entity.HasKey(e => e.ReadingStatusId);
                entity.Property(e => e.Name).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasData(
                    new ReadingStatus { ReadingStatusId = 1, Name = "TBR", SortOrder = 1 },
                    new ReadingStatus { ReadingStatusId = 2, Name = "Reading", SortOrder = 2 },
                    new ReadingStatus { ReadingStatusId = 3, Name = "Finished", SortOrder = 3 },
                    new ReadingStatus { ReadingStatusId = 4, Name = "DNF", SortOrder = 4 }
                ); 

            });
        }
    }
}
