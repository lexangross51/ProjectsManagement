using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database.EntityConfigurations;
using Projects.DataAccess.Models;

namespace Projects.DataAccess.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; } = default!;

    public DbSet<Project> Projects { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}