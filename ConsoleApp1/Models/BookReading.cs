using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLibrary.Models
{
    public class BookReading
    {
        //primary key
        public int BookReadingId { get; set; }

        //foreign keys
        public int BookId { get; set; }
        public int ReadingStatusId { get; set; }

        public DateTime DateAdded { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateFinished { get; set; }

        public int? Rating { get; set; } // 1 to 5 stars

        // Navigation
        public virtual Book Book { get; set; } = null!;
        public virtual ReadingStatus ReadingStatus { get; set; } = null!;
    }
}
