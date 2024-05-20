using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.UnitOfWork;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Projects.Queries.GetProject;

public class GetProjectQueryHandler(IUnitOfWork reposManager) : IRequestHandler<GetProjectQuery, ProjectDetailsVm>
{
    public async Task<ProjectDetailsVm> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await reposManager.Projects.GetWithExecutorsAsync(request.Id, cancellationToken);

        if (project == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        Employee? manager = null;

        if (project.ManagerId.HasValue)
        {
            manager = await reposManager.Employees.GetAsync(project.ManagerId.Value, cancellationToken);
        }

        return new ProjectDetailsVm
        {
            Id = project.Id,
            ProjectName = project.ProjectName,
            Priority = project.Priority,
            CompanyCustomer = project.CompanyCustomer,
            CompanyExecutor = project.CompanyExecutor,
            DateStart = project.DateStart,
            DateEnd = project.DateEnd,
            ManagerId = project.ManagerId,
            Manager = manager,
            Executors = project.Executors
        };
    }
}