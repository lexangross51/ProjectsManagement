using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.ProjectsStorage;

namespace Projects.Logic.Projects.Queries.GetProjectList;

public class GetProjectListQueryHandler(IProjectRepository repos) : IRequestHandler<GetProjectListQuery, ProjectListVm>
{
    public async Task<ProjectListVm> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Project>? allProjects = await repos.GetAllAsync(cancellationToken);
        var listVm = new ProjectListVm();

        if (allProjects == null) return listVm;
        
        if (request.Role == Roles.Manager)
        {
            allProjects = allProjects.Where(p => p.ManagerId == request.UserId).AsEnumerable();
        }
        else if (request.Role == Roles.Employee)
        {
            var userProjects = new List<Project>();

            foreach (var project in allProjects)
            {
                if (project.Executors is not { Count: > 0 }) continue;

                if (project.Executors.Any(e => e.Id == request.UserId))
                {
                    userProjects.Add(project);
                }
            }

            allProjects = userProjects;
        }
        
        foreach (var project in allProjects)
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