using System.Linq.Expressions;
using Projects.Logic.Common.DataFiltration;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.FilterProjects.Filters;

public enum DateType
{
    Start,
    End
}

public class DateFilter : IFilterSpecification<ProjectLookupDto>
{
    public DateOnly? DateFrom { get; init; }
    
    public DateOnly? DateTo { get; init; }
    
    public DateType DateType { get; set; }

    public Expression<Func<ProjectLookupDto, bool>> Criteria
        => p => (DateType == DateType.Start
                    ? !DateFrom.HasValue || p.DateStart >= DateFrom.Value
                    : !DateFrom.HasValue || p.DateEnd >= DateFrom.Value)
                && (DateType == DateType.Start
                    ? !DateTo.HasValue || p.DateStart <= DateTo.Value
                    : !DateTo.HasValue || p.DateEnd <= DateTo.Value);
}