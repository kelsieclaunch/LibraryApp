using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

internal static class Program
{
    public static IServiceProvider Services { get; private set; } = null!;

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        services.AddDbContext<PersonalLibrary.Data.LibraryDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ConsoleApp1;Trusted_Connection=True"));

        Services = services.BuildServiceProvider();

        Application.Run(new MainForm());
    }
}
