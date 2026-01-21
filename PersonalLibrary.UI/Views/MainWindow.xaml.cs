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
using System.Windows.Input;

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
        if (e.EditingElement is ComboBox comboBox && comboBox.SelectedValue is int newStatusId && e.Row.Item is Book book)
        {

            var bookReading = book.BookReading;

            if (bookReading == null)
            {
                return;
            }
            var previousStatusId = bookReading.ReadingStatusId;

            bookReading.ReadingStatusId = newStatusId;

            var transitionedToFinished = previousStatusId != 3 && newStatusId == 3;

            var now = DateTime.Now;

            switch (newStatusId)
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

            if (transitionedToFinished)
            {
                var ratingWindow = new RateBookWindow
                {
                    Owner = this
                };

                if(ratingWindow.ShowDialog() == true && ratingWindow.SelectedRating.HasValue)
                {
                    bookReading.Rating = ratingWindow.SelectedRating.Value;
                }
            }

            _dbContext.SaveChanges();
           
        }


    }

    private void BooksDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

        if (sender is not DataGrid dataGrid) return;

        if (dataGrid.CurrentCell.Column == null) return;

        if (dataGrid.CurrentCell.Column.Header?.ToString() != "Rating") return;


        if (dataGrid.SelectedItem  is not Book book)
        {
            return;
        }

        var bookReading = book.BookReading;
        if (bookReading == null) return;

        if (bookReading.ReadingStatusId != 3) return;

        var ratingWindow = new RateBookWindow
        {
            Owner = this
        };

        if(ratingWindow.ShowDialog() == true && ratingWindow.SelectedRating.HasValue)
        {
            bookReading.Rating = ratingWindow.SelectedRating.Value;
            _dbContext.SaveChanges();
        }
    }

   
}