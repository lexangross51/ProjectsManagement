using MediatR;
using Projects.DataAccess.Storage.TasksStorage;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class GetTaskQueryHandler(ITaskRepository repos) : IRequestHandler<GetTaskListQuery, TaskListVm>
{
    public async Task<TaskListVm> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
    {
        var allTasks = await repos.GetAllAsync(cancellationToken);
        var taskListVm = new TaskListVm();

        if (allTasks == null) return taskListVm;

        foreach (var task in allTasks)
        {
            taskListVm.Tasks.Add(new TaskLookupDto
            {
                Id = task.Id,
                TaskName = task.TaskName,
                Priority = task.Priority,
                Status = task.Status
            });
        }

        return taskListVm;
    }
}