using MediatR;

namespace Projects.Logic.Tasks.Queries.GetTask;

public class GetTaskQuery : IRequest<TaskDetailsVm>
{
    public Guid Id { get; init; }
}