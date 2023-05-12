using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.WebApi.Models;

namespace TaskApp.WebApi.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description).HasColumnType("VARCHAR(100)");
        builder.Property(p => p.Category).HasColumnType("VARCHAR(50)");        
    }
}
