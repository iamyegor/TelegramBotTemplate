using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        char separator = Path.DirectorySeparatorChar;
        string basePath = Directory.GetCurrentDirectory() + $"{separator}..{separator}Api";

        Console.WriteLine($"basePath: {basePath}");

        return Create(basePath);
    }

    private ApplicationDbContext Create(string basePath)
    {
        string evironmentName = Environment.GetEnvironmentVariable(AspNetCoreEnvironment)!;
        Console.WriteLine($"Environment: {evironmentName}");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.local.json", optional: true)
            .AddJsonFile($"appsettings.{evironmentName}.json", optional: true)
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
