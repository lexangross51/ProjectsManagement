using Projects.DataAccess.Models;
using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Logic.Tasks.Queries.GetTask;

public class TaskDetailsVm
{
    public Guid Id { get; init; }

    public string TaskName { get; init; } = string.Empty;

    public uint Priority { get; init; }

    public TaskStatus Status { get; init; }

    public string? Description { get; init; }

    public Guid AuthorId { get; init; }

    public Employee Author { get; init; } = default!;

    public Guid? ExecutorId { get; init; }

    public Employee? Executor { get; init; }
}