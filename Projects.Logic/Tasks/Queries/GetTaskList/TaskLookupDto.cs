using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class TaskLookupDto
{
    public Guid Id { get; init; }

    public string TaskName { get; init; } = string.Empty;

    public TaskStatus Status { get; init; }

    public uint Priority { get; init; }
}