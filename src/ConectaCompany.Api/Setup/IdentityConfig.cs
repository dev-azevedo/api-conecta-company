using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Context;
using ConectaCompany.Infra.Database.Utils;
using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Api.Setup;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }
    
    public static async void UseIdentityConfig(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        await DbInitializer.SeedRoles(roleManager);
    }
}