using System.Linq.Expressions;
using ConectaCompany.Domain.Models;
using ConectaCompany.Infra.Database.FluentApi;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConectaCompany.Infra.Database.Context;

public class AppDbContext : IdentityDbContext<User, Role, long>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var activeProperty = entityType.FindProperty("Active");
            if (activeProperty != null && activeProperty.ClrType == typeof(bool))
            {
                // cria expressão equivalente a: (x => x.Active)
                var parameter = Expression.Parameter(entityType.ClrType, "x");
                var property = Expression.Property(parameter, "Active");
                var comparison = Expression.Equal(property, Expression.Constant(true));
                var lambda = Expression.Lambda(comparison, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}