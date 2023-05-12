using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.WebApi.Models;

namespace TaskApp.WebApi.Configurations;

public sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");
        builder.HasKey(x => x.Id);

        builder.HasOne(p=> p.Order)
            .WithMany(p=> p.OrderDetails)
            .HasForeignKey(p=>p.OrderId);
    }
}
