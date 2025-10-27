using ConectaCompany.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConectaCompany.Infra.Database.FluentApi;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Content).IsRequired();
        
        builder.Property(p => p.PathImage).HasMaxLength(500);
        
        builder.HasOne(p => p.CreatedBy)
            .WithMany(e => e.CreatedPosts)
            .HasForeignKey(p => p.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.UpdatedBy)
            .WithMany(e => e.UpdatedPosts)
            .HasForeignKey(p => p.UpdatedById)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Company)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
            
    }   
}