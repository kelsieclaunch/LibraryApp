using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PersonalLibrary.UI.Views
{
 
    public partial class RateBookWindow : Window
    {
        public int? SelectedRating { get; private set; }

        public int HoveredRating { get; set; } = 0;
        public RateBookWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag.ToString(), out int rating))
            {
                SelectedRating = rating;
                DialogResult = true;
            }
        }

        private void Star_MouseEnter(object sender, MouseEventArgs e)
        {
            if(sender is Button button && int.TryParse(button.Tag.ToString(), out int rating))
            {
                HoveredRating = rating;
                UpdateStars();
            }
        }
        private void Star_MouseLeave(object sender, MouseEventArgs e)
        {
            HoveredRating = 0;
            UpdateStars();
        }

        private void UpdateStars()
        {
            for(int i = 1; i <= 5; i++)
            {
                var starButton = this.FindName($"Star{i}") as Button;
                if (starButton != null)
                {
                    starButton.Content = (i <= (HoveredRating > 0 ? HoveredRating : SelectedRating ?? 0)) ? "★" : "☆";
                       
                }
            }
        }
    }
}
