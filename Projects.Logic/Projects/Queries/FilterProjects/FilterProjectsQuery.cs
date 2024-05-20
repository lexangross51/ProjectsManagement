using MediatR;
using Projects.Logic.Projects.Queries.FilterProjects.Filters;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.FilterProjects;

public class FilterProjectsQuery : IRequest<ProjectListVm>
{
    public ProjectListVm ProjectListVm { get; init; } = default!;
    
    public IEnumerable<IFilterSpecification<ProjectLookupDto>> Filters { get; init; } = default!;
}