using MediatR;
using Projects.DataAccess.Models.Tasks;
using Projects.DataAccess.Storage.TasksStorage;

namespace Projects.Logic.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler(ITaskRepository repos) : IRequestHandler<CreateTaskCommand, Guid>
{
    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new ProjectTask
        {
            TaskName = request.TaskName,
            AuthorId = request.AuthorId,
            Priority = request.Priority,
            Description = request.Description,
            ExecutorId = request.ExecutorId
        };

        await repos.SaveAsync(task, cancellationToken);

        return task.Id;
    }
}