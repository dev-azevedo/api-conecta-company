using ConectaCompany.Domain.Models;
using ConectaCompany.Shared.Constants;
using Microsoft.AspNetCore.Identity;

namespace ConectaCompany.Infra.Database.Utils;

public static class DbInitializer
{
    public static async Task SeedRoles(RoleManager<Role> roleManager)
    {
        foreach (var roleName in Roles.All)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new Role { Name = roleName };
                await roleManager.CreateAsync(role);
            }
        }
    }
}