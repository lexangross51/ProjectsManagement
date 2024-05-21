using Projects.DataAccess.Models.Base;

namespace Projects.DataAccess.Models.Tasks;

public class ProjectTask : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string TaskName { get; set; } = string.Empty;

    public uint Priority { get; set; }

    public TaskStatus Status { get; set; }

    public string? Description { get; set; }

    public Guid AuthorId { get; init; }

    public Employee Author { get; init; } = default!;

    public Guid? ExecutorId { get; set; }

    public Employee? Executor { get; set; }

    public Guid ProjectId { get; init; }

    public Project Project { get; init; } = default!;
}