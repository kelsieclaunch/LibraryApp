using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Data;
using PersonalLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PersonalLibrary.UI.Commands;
using PersonalLibrary.UI.Views;
using PersonalLibrary.UI.ViewModels;

namespace PersonalLibrary.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly LibraryDbContext _dbContext;
        public ObservableCollection<Book> Books { get; }
        public ObservableCollection<ReadingStatus> ReadingStatuses { get; }

        public ICommand AddBookCommand { get; }
        public MainViewModel(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;

            Books = new ObservableCollection<Book>(
                _dbContext.Books
                .Include(b => b.BookReading)
                .ThenInclude(br => br.ReadingStatus)
                .ToList());

            foreach (var book in Books)
            {
                if(book.BookReading == null)
                {
                    book.BookReading = new BookReading
                    {
                        ReadingStatusId = 1,
                        BookId = book.BookId,
                        DateStarted = null,
                        DateFinished = null,
                        DateAdded = DateTime.Now

                    };
                    _dbContext.BookReadings.Add(book.BookReading);
                }
            }

            _dbContext.SaveChanges();

            ReadingStatuses = new ObservableCollection<ReadingStatus>(
                _dbContext.ReadingStatuses
                .AsNoTracking()
                .ToList());

            AddBookCommand = new RelayCommand(OpenAddBookWindow);
        }

        private void OpenAddBookWindow()
        {
            var window = new AddBookWindow(_dbContext)
            {
                Owner = App.Current.MainWindow
            };

            window.ShowDialog();

            RefreshBooks();
        }

        private void RefreshBooks()
        {
            Books.Clear();
            var books = _dbContext.Books
                .Include(b => b.BookReading)
                .ThenInclude(br => br.ReadingStatus)
                .AsNoTracking().ToList();

            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

    }
}
