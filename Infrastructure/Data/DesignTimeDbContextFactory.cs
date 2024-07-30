using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        char separator = Path.DirectorySeparatorChar;
        string basePath = Directory.GetCurrentDirectory() + $"{separator}..{separator}Api";

        Console.WriteLine($"basePath: {basePath}");

        return Create(basePath);
    }

    private ApplicationDbContext Create(string basePath)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.local.json", true)
            .AddEnvironmentVariables()
            .Build();

        string? connectionString = configuration.GetConnectionString(ConnectionString.Name);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException(
                $"Connection string '{ConnectionString.Name}' is null or empty.",
                nameof(connectionString)
            );
        }

        Console.WriteLine(
            $"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'."
        );

        return new ApplicationDbContext(connectionString);
    }
}
