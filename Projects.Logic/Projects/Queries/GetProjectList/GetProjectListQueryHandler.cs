using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.ProjectsStorage;

namespace Projects.Logic.Projects.Queries.GetProjectList;

public class GetProjectListQueryHandler(IProjectRepository repos) : IRequestHandler<GetProjectListQuery, ProjectListVm>
{
    public async Task<ProjectListVm> Handle(GetProjectListQuery request, CancellationToken cancellationToken)
    {
        var projects = await repos.GetAllAsync(cancellationToken);
        var listVm = new ProjectListVm();

        if (projects == null) return listVm;

        if (request.Role == Roles.Manager)
        {
            projects = projects.Where(p => p.ManagerId == request.UserId);
        }
        else if (request.Role == Roles.Employee)
        {
            var executorsId = new List<Guid>();

            foreach (var project in projects)
            {
                if (project.Executors == null || project.Executors.Count == 0) continue;
                
                executorsId.AddRange(project.Executors.Select(e => e.Id));
            }
            
            projects = projects.Where(_ => executorsId.Contains(request.UserId));
        }
        
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