using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalLibrary.Data;
using System.Windows;

namespace PersonalLibrary.UI;

public partial class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();

        services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PersonalLibrary;Trusted_Connection=True;"));
        Services = services.BuildServiceProvider();
        base.OnStartup(e);
    }
}