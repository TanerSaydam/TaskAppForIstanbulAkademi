using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.WebApi.Models;

namespace TaskApp.WebApi.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(p => p.Id); //yazmak zorunlu değil bro
        
        builder.HasMany(p=> p.OrderDetails)
            .WithOne(p=> p.Order)
            .HasForeignKey(p=> p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
