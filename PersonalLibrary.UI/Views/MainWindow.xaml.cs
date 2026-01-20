using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Data;
using PersonalLibrary.UI.ViewModels;
using System.Windows;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PersonalLibrary.UI.Views;

public partial class MainWindow : Window
{
    private readonly LibraryDbContext _dbContext;
    public MainWindow()
    {
        InitializeComponent();

        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _dbContext = new LibraryDbContext(options);

        DataContext = new MainViewModel(_dbContext);
    }
}