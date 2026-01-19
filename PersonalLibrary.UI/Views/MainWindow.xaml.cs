using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Data;
using PersonalLibrary.UI.ViewModels;
using System.Windows;

namespace PersonalLibrary.UI.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var db = ((App)Application.Current).Services.GetRequiredService<LibraryDbContext>();

        DataContext = new MainViewModel(db);
    }
}