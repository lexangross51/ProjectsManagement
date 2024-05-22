using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.TasksStorage;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class GetTaskQueryHandler(ITaskRepository repos) : IRequestHandler<GetTaskListQuery, TaskListVm>
{
    public async Task<TaskListVm> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
    {
        var allTasks = await repos.GetAllAsync(cancellationToken);
        var taskListVm = new TaskListVm();

        if (allTasks == null) return taskListVm;

        if (request.Role == Roles.Manager)
        {
            allTasks = allTasks.Where(t => t.AuthorId == request.UserId);
        }
        else if (request.Role == Roles.Employee)
        {
            allTasks = allTasks.Where(t => t.ExecutorId == request.UserId);
        }
        
        foreach (var task in allTasks)
        {
            taskListVm.Tasks.Add(new TaskLookupDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                TaskName = task.TaskName,
                Priority = task.Priority,
                Status = task.Status
            });
        }

        return taskListVm;
    }
}