using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.Context;
using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Api.Setup;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>();
    }
}