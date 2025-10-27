using ConectaCompany.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaCompany.Infra.Database.FluentApi;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);
        
        builder.HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.UserId);

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId);
        
        builder.HasMany(e => e.CreatedPosts)
            .WithOne(p => p.CreatedBy)
            .HasForeignKey(p => p.CreatedById);

        builder.HasMany(e => e.UpdatedPosts)
            .WithOne(p => p.UpdatedBy)
            .HasForeignKey(p => p.UpdatedById);
    }
}