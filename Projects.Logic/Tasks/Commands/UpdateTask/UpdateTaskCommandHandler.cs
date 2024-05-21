using MediatR;
using Projects.DataAccess.Models.Tasks;
using Projects.DataAccess.Storage.TasksStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler(ITaskRepository repos) : IRequestHandler<UpdateTaskCommand>
{
    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await repos.GetAsync(request.Id, cancellationToken) ??
                   throw new NotFoundException(nameof(ProjectTask), request.Id);

        task.TaskName = request.TaskName;
        task.Priority = request.Priority;
        task.Status = request.Status;
        task.Description = request.Description;
        task.ExecutorId = request.ExecutorId;

        await repos.UpdateAsync(task, cancellationToken);
    }
}