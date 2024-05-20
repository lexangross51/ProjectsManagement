using MediatR;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Logic.Tasks.Queries.SortTasks;

public class SortTasksQuery : IRequest<TaskListVm>
{
    public TaskListVm TaskListVm { get; init; } = default!; // Data to sort

    public string Column { get; init; } = string.Empty;

    public string Order { get; init; } = string.Empty;
}