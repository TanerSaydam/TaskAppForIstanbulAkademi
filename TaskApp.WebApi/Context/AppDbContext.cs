using Microsoft.EntityFrameworkCore;
using TaskApp.WebApi.Models;

namespace TaskApp.WebApi.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}
    //Context.Set<Product>()
    //context.Products
    //public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
    }
}
