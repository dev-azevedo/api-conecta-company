using ConectaCompany.Application.Interfaces;
using ConectaCompany.Application.Services;
using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Infra.Data.Repositories;
using ConectaCompany.Infra.Database.Repositories;

namespace ConectaCompany.Api.Setup;

public static class DiConfig
{
    public static void AddDiConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddServices();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}