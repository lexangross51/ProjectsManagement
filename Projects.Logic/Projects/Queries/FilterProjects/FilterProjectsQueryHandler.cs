using MediatR;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.FilterProjects;

public class FilterProjectsQueryHandler : IRequestHandler<FilterProjectsQuery, ProjectListVm>
{
    public async Task<ProjectListVm> Handle(FilterProjectsQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            var filters = request.Filters;
            var listVm = request.ProjectListVm;
            var projects = listVm.Projects;
            var filtered = projects.AsEnumerable();

            foreach (var filter in filters)
            {
                filtered = filtered.Where(filter.Criteria.Compile());
            }

            var filteredListVm = new ProjectListVm { Projects = filtered.ToList() };
            return filteredListVm;
        }, cancellationToken);

        return await task;
    }
}