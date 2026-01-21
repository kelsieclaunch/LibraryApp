using PersonalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace PersonalLibrary.UI.Views
{
    public partial class EditBookWindow : Window
    {
        private readonly Book _book;
        public Book EditedBook => _book;

        private int _selectedRating = 0;
        public EditBookWindow(Book book)
        {
            InitializeComponent();
            _book = book;

            // set initial values
            TitleTextBox.Text = _book.Title;
            AuthorTextBox.Text = _book.Author;
            ISBNTextBox.Text = _book.ISBN;
            PublicationYearTextBox.Text = _book.PublicationYear?.ToString();

            StatusComboBox.ItemsSource = _book.BookReading.ReadingStatus?.BookReadings ?? throw new InvalidOperationException();
            StatusComboBox.SelectedItem = _book.BookReading.ReadingStatusId;

            DateStartedPicker.SelectedDate = _book.BookReading.DateStarted;
            DateFinishedPicker.SelectedDate = _book.BookReading.DateFinished;

            _selectedRating = _book.BookReading.Rating ?? 0;
            UpdateStars();

        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn && int.TryParse(btn.Tag.ToString(), out int rating))
            {
                _selectedRating = rating;
                UpdateStars();
            }
        }

        private void UpdateStars()
        {
            for (int i = 1; i <=5; i++)
            {
                var element = this.FindName($"Star{i}");
                if(element is Button starBtn)
                {
                    starBtn.Content = i <= _selectedRating ? "★" : "☆";

                }
                
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // update values
            _book.Title = TitleTextBox.Text;
            _book.Author = AuthorTextBox.Text;
            _book.ISBN = ISBNTextBox.Text;
            if (int.TryParse(PublicationYearTextBox.Text, out int year)) _book.PublicationYear = year;

            if (StatusComboBox.SelectedValue is int statusId) _book.BookReading.ReadingStatusId = statusId;

            _book.BookReading.DateStarted = DateStartedPicker.SelectedDate;
            _book.BookReading.DateFinished = DateFinishedPicker.SelectedDate;

            _book.BookReading.Rating = _selectedRating;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



    }
}
