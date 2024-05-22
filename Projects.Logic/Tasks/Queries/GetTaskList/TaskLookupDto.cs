using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public readonly struct TaskLookupDto
{
    public Guid Id { get; init; }
    
    public Guid ProjectId { get; init; }

    public string TaskName { get; init; }

    public TaskStatus Status { get; init; }

    public uint Priority { get; init; }
}