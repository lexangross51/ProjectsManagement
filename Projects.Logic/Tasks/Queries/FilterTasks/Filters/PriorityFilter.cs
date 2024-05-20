using System.Linq.Expressions;
using Projects.Logic.Common.DataFiltration;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Logic.Tasks.Queries.FilterTasks.Filters;

public class PriorityFilter : IFilterSpecification<TaskLookupDto>
{
    public uint? PriorityFrom { get; init; }

    public uint? PriorityTo { get; init; }

    public Expression<Func<TaskLookupDto, bool>> Criteria
        => t => (!PriorityFrom.HasValue || t.Priority >= PriorityFrom.Value) &&
                (!PriorityTo.HasValue || t.Priority <= PriorityTo.Value);
}