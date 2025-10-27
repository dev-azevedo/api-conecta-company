using ConectaCompany.Application.Interfaces;
using ConectaCompany.Application.Services;
using ConectaCompany.Domain.Interfaces;
using ConectaCompany.Infra.Data.Repositories;
using ConectaCompany.Infra.Database.Repositories;
using ConectaCompany.Infra.ExternalServices.Interfaces;
using ConectaCompany.Infra.ExternalServices.Services;

namespace ConectaCompany.Api.Setup;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddServices();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        // External Services
        services.AddScoped<IJwtService, JwtService>();
    }
}