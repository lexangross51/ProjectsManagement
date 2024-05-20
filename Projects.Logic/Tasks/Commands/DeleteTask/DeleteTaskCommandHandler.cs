using MediatR;
using Projects.DataAccess.Models.Tasks;
using Projects.DataAccess.Storage.TasksStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler(ITaskRepository repos) : IRequestHandler<DeleteTaskCommand>
{
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await repos.GetAsync(request.Id, cancellationToken);

        if (task == null)
        {
            throw new NotFoundException(nameof(ProjectTask), request.Id);
        }

        await repos.DeleteAsync(request.Id, cancellationToken);
    }
}