using MediatR;

namespace Projects.Logic.Tasks.Commands.DeleteTask;

public class DeleteTaskCommand : IRequest
{
    public Guid Id { get; init; }
}