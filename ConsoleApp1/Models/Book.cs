using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalLibrary.Models
{
    public class Book
    {
        // BookId is primary key. Title and Author are required. ISBN and publication year are nullable
        public int BookId { get; set; }

        public string Title { get; set; } = null!;
        public string Author { get; set; }= null!;

        public string? ISBN { get; set; }
        public int? PublicationYear { get; set; }

        // Navigation
        public ICollection<BookReading> BookReadings { get; set; } = new List<BookReading>();
    }
}
