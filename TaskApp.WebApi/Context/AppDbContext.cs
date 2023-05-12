using Microsoft.EntityFrameworkCore;

namespace TaskApp.WebApi.Context;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
