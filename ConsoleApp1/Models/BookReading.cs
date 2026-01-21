using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PersonalLibrary.Models
{
    public class BookReading : INotifyPropertyChanged
    {
        //primary key
        public int BookReadingId { get; set; }

        //foreign keys
        public int BookId { get; set; }

        private int _readingStatusId;
        public int ReadingStatusId
        {
            get => _readingStatusId;
            set
            {
                if (_readingStatusId != value)
                {
                    _readingStatusId = value;
                    OnPropertyChanged(nameof(ReadingStatusId));
                }

            }
        }

        private DateTime? _dateStarted;
        public DateTime? DateStarted
        {
            get => _dateStarted;
            set
            {
                if (_dateStarted != value)
                {
                    _dateStarted = value;
                    OnPropertyChanged(nameof(DateStarted));
                }
            }
        }

        private DateTime? _dateFinished;
        public DateTime? DateFinished
        {
            get => _dateFinished;
            set
            {
                if (_dateFinished != value)
                {
                    _dateFinished = value;
                    OnPropertyChanged(nameof(DateFinished));
                }
            }
        }

        private int? _rating;
        public int? Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                    OnPropertyChanged(nameof(RatingStars));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Navigation
        public virtual Book Book { get; set; } = null!;
        public virtual ReadingStatus ReadingStatus { get; set; } = null!;

        // Rating Stars
        public string RatingStars
        {
            get
            {
                int rating = Rating ?? 0;
                return new string('★', rating) + new string('☆', 5 - rating);
                
            }
        }
    }
}
