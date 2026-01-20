using PersonalLibrary.Data;
using System.Windows;
using PersonalLibrary.UI.ViewModels;

namespace PersonalLibrary.UI.Views
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow(LibraryDbContext dbContext)
        {
            InitializeComponent();
            DataContext = new AddBookViewModel(dbContext);
        }
    }
}
