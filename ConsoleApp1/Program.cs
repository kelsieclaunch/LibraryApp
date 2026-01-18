using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Data;

var services = new ServiceCollection();

services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PersonalLibrary;Trusted_Connection=True;"));

using var serviceProvider = services.BuildServiceProvider();

//create db context
using var db = serviceProvider.GetRequiredService<LibraryDbContext>();

Console.WriteLine("EF Core DbContext initialized successfully.");
    