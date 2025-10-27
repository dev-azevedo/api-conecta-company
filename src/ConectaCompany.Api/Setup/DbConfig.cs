using ConectaCompany.Infra.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ConectaCompany.Api.Setup;

public static class DbConfig
{
    public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var dbName = configuration["Database:Name"] ?? throw new ArgumentNullException("Database:Name");
        var databasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", dbName);
        services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));
    }
}