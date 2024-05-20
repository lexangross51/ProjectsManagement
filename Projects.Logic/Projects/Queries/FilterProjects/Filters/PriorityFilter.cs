using System.Linq.Expressions;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.FilterProjects.Filters;

public class PriorityFilter : IFilterSpecification<ProjectLookupDto>
{
    public int? PriorityFrom { get; init; }

    public int? PriorityTo { get; init; }
    
    public Expression<Func<ProjectLookupDto, bool>> Criteria 
        => p => (!PriorityFrom.HasValue || p.Priority >= PriorityFrom.Value) && 
                (!PriorityTo.HasValue || p.Priority <= PriorityTo.Value);
}