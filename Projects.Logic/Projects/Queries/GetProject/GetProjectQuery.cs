using MediatR;

namespace Projects.Logic.Projects.Queries.GetProject;

public class GetProjectQuery : IRequest<ProjectDetailsVm>
{
    public Guid Id { get; init; }
}