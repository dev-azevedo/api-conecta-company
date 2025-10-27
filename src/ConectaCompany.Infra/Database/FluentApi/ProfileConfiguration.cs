using ConectaCompany.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaCompany.Infra.Database.FluentApi;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Role)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(p => p.Employees)
            .WithOne(e => e.Profile)
            .HasForeignKey(e => e.ProfileId);
    } 
}