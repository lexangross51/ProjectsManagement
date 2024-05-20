using MediatR;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.SortProjects;

public class SortProjectsQueryHandler : IRequestHandler<SortProjectsQuery, ProjectListVm>
{
    public async Task<ProjectListVm> Handle(SortProjectsQuery request, CancellationToken cancellationToken)
    {
        var task = Task.Run(() =>
        {
            var projects = request.ProjectListVm.Projects;

            var ordered = request.Column switch
            {
                "ProjectName" => request.Order == "asc"
                    ? projects.OrderBy(p => p.ProjectName)
                    : projects.OrderByDescending(p => p.ProjectName),
                "Priority" => request.Order == "asc"
                    ? projects.OrderBy(p => p.Priority)
                    : projects.OrderByDescending(p => p.Priority),
                "DateStart" => request.Order == "asc"
                    ? projects.OrderBy(p => p.DateStart)
                    : projects.OrderByDescending(p => p.DateStart),
                "DateEnd" => request.Order == "asc"
                    ? projects.OrderBy(p => p.DateEnd)
                    : projects.OrderByDescending(p => p.DateEnd),
                _ => projects.AsEnumerable()
            };

            var sortedListVm = new ProjectListVm { Projects = ordered.ToList() };
            return sortedListVm;
        }, cancellationToken);

        return await task;
    }
}