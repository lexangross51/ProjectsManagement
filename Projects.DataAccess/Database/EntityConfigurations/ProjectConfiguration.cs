using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.DataAccess.Models;

namespace Projects.DataAccess.Database.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProjectName).HasMaxLength(256);
        builder.HasOne(p => p.Manager)
            .WithMany(e => e.ManagedProjects)
            .HasForeignKey(p => p.ManagerId);
        builder.HasMany(p => p.Executors)
            .WithMany(e => e.Projects)
            .UsingEntity<Dictionary<string, object>>(
                "ProjectEmployee",
                j => j
                    .HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey("EmployeeId"),
                j => j
                    .HasOne<Project>()
                    .WithMany()
                    .HasForeignKey("ProjectId"));
    }
}