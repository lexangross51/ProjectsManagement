using MediatR;
using Projects.DataAccess.Storage.ProjectsStorage;

namespace Projects.Logic.Projects.Queries.GetProjectList;

public class GetProjectListQueryHandler(IProjectRepository repos) : IRequestHandler<GetProjectListQuery, ProjectListVm>
{
    public async Task<ProjectListVm> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
    {
        var projects = await repos.GetAllAsync(cancellationToken);
        var listVm = new ProjectListVm();

        if (projects == null) return listVm;

        foreach (var project in projects)
        {
            listVm.Projects.Add(new ProjectLookupDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Priority = project.Priority,
                DateStart = project.DateStart,
                DateEnd = project.DateEnd
            });
        }

        return listVm;
    }
}