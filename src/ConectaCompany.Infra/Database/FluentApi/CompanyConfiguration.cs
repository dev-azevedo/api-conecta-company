using ConectaCompany.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaCompany.Infra.Database.FluentApi;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.CNPJ)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(c => c.Posts)
            .WithOne(p => p.Company)
            .HasForeignKey(p => p.CompanyId);
        
        builder.HasMany(c => c.Employees)
            .WithOne(u => u.Company)
            .HasForeignKey(u => u.CompanyId);
    }
}