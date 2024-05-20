using MediatR;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Logic.Tasks.Queries.SortTasks;

public class SortTasksQueryHandler : IRequestHandler<SortTasksQuery, TaskListVm>
{
    public async Task<TaskListVm> Handle(SortTasksQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            var tasks = request.TaskListVm.Tasks;

            var ordered = request.Column switch
            {
                "TaskName" => request.Order == "asc"
                    ? tasks.OrderBy(p => p.TaskName)
                    : tasks.OrderByDescending(p => p.TaskName),
                "Priority" => request.Order == "asc"
                    ? tasks.OrderBy(p => p.Priority)
                    : tasks.OrderByDescending(p => p.Priority),
                "Status" => request.Order == "asc"
                    ? tasks.OrderBy(p => p.Status)
                    : tasks.OrderByDescending(p => p.Status),
                _ => tasks.AsEnumerable()
            };

            var sortedListVm = new TaskListVm { Tasks = ordered.ToList() };
            return sortedListVm;
        }, cancellationToken);

        return await task;
    }
}