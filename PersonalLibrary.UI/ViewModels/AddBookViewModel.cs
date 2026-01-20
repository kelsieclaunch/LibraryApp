using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Data;
using PersonalLibrary.Models;
using PersonalLibrary.UI.Commands;

namespace PersonalLibrary.UI.ViewModels
{
    public class AddBookViewModel : ViewModelBase
    {
        private readonly LibraryDbContext _dbContext;

        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int? PublicationYear { get; set; }

        public ICommand SaveCommand { get; }

        public AddBookViewModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
            {
                MessageBox.Show("Title and Author are required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                ISBN = ISBN,
                PublicationYear = PublicationYear
            };

            _dbContext.Books.Add(newBook);
            _dbContext.SaveChanges();

            var tbrStatus = _dbContext.ReadingStatuses.Single(rs => rs.Name == "TBR");

            var bookReading = new BookReading
            {
                BookId = newBook.BookId,
                ReadingStatusId = tbrStatus.ReadingStatusId,
                DateAdded = DateTime.Now
            };

            _dbContext.BookReadings.Add(bookReading);
            _dbContext.SaveChanges();

            CloseWindow();

        }

        private void CloseWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                    break;
                }
            }

        }
    }
}
