using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Data;
using PersonalLibrary.UI.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;
using PersonalLibrary.Models;

namespace PersonalLibrary.UI.Views;

public partial class MainWindow : Window
{
    private readonly LibraryDbContext _dbContext;
    public MainWindow()
    {
        InitializeComponent();


        var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=PersonalLibraryDb;Trusted_Connection=True;";

        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        _dbContext = new LibraryDbContext(options);

        _dbContext.Database.EnsureCreated();

        DataContext = new MainViewModel(_dbContext);
    }

    private void BooksDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
        if (e.Row.Item is Book book)
        {
            var bookReading = _dbContext.BookReadings.Find(book.BookReading.BookReadingId);
            if (bookReading != null)
            {
                bookReading.ReadingStatusId = book.BookReading.ReadingStatusId;
                _dbContext.SaveChanges();
            }
        }
    }
}