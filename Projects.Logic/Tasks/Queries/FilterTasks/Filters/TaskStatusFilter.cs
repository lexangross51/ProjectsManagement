using System.Linq.Expressions;
using Projects.Logic.Common.DataFiltration;
using Projects.Logic.Tasks.Queries.GetTaskList;
using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Logic.Tasks.Queries.FilterTasks.Filters;

public class TaskStatusFilter : IFilterSpecification<TaskLookupDto>
{
    public TaskStatus? Status { get; init; } = null;
    
    public Expression<Func<TaskLookupDto, bool>> Criteria
        => t => !Status.HasValue || Status.Value == t.Status;
}