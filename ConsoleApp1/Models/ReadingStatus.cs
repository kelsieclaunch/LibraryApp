using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalLibrary.Models
{
    public class ReadingStatus
    {
        public int ReadingStatusId { get; set; } // primary key

        public string Name { get; set; } = null!; // TBR, Reading, Finished, DNF
        public int SortOrder { get; set; } // 1- 4 (for now)

        //Navigation
        public ICollection<BookReading> BookReadings { get; set; } = new List<BookReading>();

    }
}
