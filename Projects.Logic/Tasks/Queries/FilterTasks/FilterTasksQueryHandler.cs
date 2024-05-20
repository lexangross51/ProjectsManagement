using MediatR;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Logic.Tasks.Queries.FilterTasks;

public class FilterTasksQueryHandler : IRequestHandler<FilterTasksQuery, TaskListVm>
{
    public async Task<TaskListVm> Handle(FilterTasksQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            var filters = request.Filters;
            var listVm = request.TaskListVm;
            var tasks = listVm.Tasks;
            var filtered = tasks.AsEnumerable();

            foreach (var filter in filters)
            {
                filtered = filtered.Where(filter.Criteria.Compile());
            }

            var filteredListVm = new TaskListVm { Tasks = filtered.ToList() };
            return filteredListVm;
        }, cancellationToken);

        return await task;
    }
}