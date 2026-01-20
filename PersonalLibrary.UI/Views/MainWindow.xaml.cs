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
        if (e.Row.Item is not Book book || book.BookReading == null)
        {
            return;
        }

        var bookReading = _dbContext.BookReadings.FirstOrDefault(br => br.BookReadingId == book.BookReading.BookReadingId);

        if (bookReading == null)
        {
            return;
        }

        var now = DateTime.Now;

        switch (book.BookReading.ReadingStatusId)
        {
            case 1: //TBR
                bookReading.DateStarted = null;
                bookReading.DateFinished = null;
                break;

            case 2: //Reading
                bookReading.DateStarted ??= now;
                bookReading.DateFinished = null;
                break;

            case 3: //Finished
                if (bookReading.DateStarted == null)
                {
                    bookReading.DateStarted = now;
                }
                if (bookReading.DateFinished == null)
                {
                    bookReading.DateFinished = now;
                }
                break;

            case 4: //DNF
                bookReading.DateFinished = null;
                break;

        }

        bookReading.ReadingStatusId = book.BookReading.ReadingStatusId;
        _dbContext.SaveChanges();
    }
}