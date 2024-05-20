using MediatR;
using Projects.Logic.Common.DataFiltration;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Logic.Tasks.Queries.FilterTasks;

public class FilterTasksQuery : IRequest<TaskListVm>
{
    public TaskListVm TaskListVm { get; init; } = default!;

    public IEnumerable<IFilterSpecification<TaskLookupDto>> Filters { get; init; } = default!;
}