using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.DataAccess.Models.Tasks;

namespace Projects.DataAccess.Database.EntityConfigurations;

public class TaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TaskName).HasMaxLength(256);
        builder.Property(t => t.Description).HasMaxLength(512);
        builder.HasOne(t => t.Author)
            .WithMany(e => e.CreatedTasks)
            .HasForeignKey(t => t.AuthorId);
        builder.HasOne(t => t.Executor)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.ExecutorId);
        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);
    }
}