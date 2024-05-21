using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.ProjectsStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler(IProjectRepository repos) : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        _ = await repos.GetAsync(request.Id, cancellationToken) ??
                      throw new NotFoundException(nameof(Project), request.Id);

        await repos.DeleteAsync(request.Id, cancellationToken);
    }
}