using Microsoft.EntityFrameworkCore;
using PersonalLibrary.Data;
using PersonalLibrary.Models;
using System.Collections.ObjectModel;

namespace PersonalLibrary.UI.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Book> Books { get; }
        public MainViewModel(LibraryDbContext db)
        {
           
            Books = new ObservableCollection<Book>(db.Books.AsNoTracking().ToList());
        }

    }
}
