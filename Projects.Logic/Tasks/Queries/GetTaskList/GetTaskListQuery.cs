using MediatR;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class GetTaskListQuery : IRequest<TaskListVm>
{
    public Guid UserId { get; init; }

    public string Role { get; set; } = string.Empty;
}