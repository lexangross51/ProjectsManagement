using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Models.Tasks;
using Projects.DataAccess.Storage.UnitOfWork;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Tasks.Queries.GetTask;

public class GetTaskQueryHandler(IUnitOfWork reposManager) : IRequestHandler<GetTaskQuery, TaskDetailsVm>
{
    public async Task<TaskDetailsVm> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await reposManager.Tasks.GetAsync(request.Id, cancellationToken) ??
                   throw new NotFoundException(nameof(ProjectTask), request.Id);
        var author = await reposManager.Employees.GetAsync(task.AuthorId, cancellationToken);
        var project = await reposManager.Projects.GetAsync(task.ProjectId, cancellationToken);

        Employee? executor = null;

        if (task.ExecutorId.HasValue)
        {
            executor = await reposManager.Employees.GetAsync(task.ExecutorId.Value, cancellationToken);
        }

        return new TaskDetailsVm
        {
            Id = task.Id,
            TaskName = task.TaskName,
            Priority = task.Priority,
            Status = task.Status,
            Description = task.Description,
            AuthorId = task.AuthorId,
            Author = author!,
            ExecutorId = task.ExecutorId,
            Executor = executor,
            ProjectId = task.ProjectId,
            Project = project!
        };
    }
}