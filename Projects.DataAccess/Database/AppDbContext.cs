using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database.EntityConfigurations;
using Projects.DataAccess.Models;
using Projects.DataAccess.Models.Tasks;

namespace Projects.DataAccess.Database;

public sealed class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Employee> Employees { get; set; } = default!;

    public DbSet<Project> Projects { get; set; } = default!;
    
    public DbSet<ProjectTask> Tasks { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}