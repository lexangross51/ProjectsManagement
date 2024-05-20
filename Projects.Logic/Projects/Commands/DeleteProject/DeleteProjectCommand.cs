using MediatR;

namespace Projects.Logic.Projects.Commands.DeleteProject;

public class DeleteProjectCommand : IRequest
{
    public Guid Id { get; init; }
}