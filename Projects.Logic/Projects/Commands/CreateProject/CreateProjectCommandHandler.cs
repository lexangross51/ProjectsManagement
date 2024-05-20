using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.UnitOfWork;

namespace Projects.Logic.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler(IUnitOfWork reposManager) : IRequestHandler<CreateProjectCommand, Guid>
{
    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            ProjectName = request.ProjectName,
            DateStart = request.DateStart,
            DateEnd = request.DateEnd,
            Priority = request.Priority,
            CompanyCustomer = request.CompanyCustomer,
            CompanyExecutor = request.CompanyExecutor,
            ManagerId = request.ManagerId
        };

        if (request.ExecutorsId != null)
        {
            var employees = await reposManager.Employees.GetAllAsync(cancellationToken);
            employees = employees.Where(e => request.ExecutorsId.Contains(e.Id));
            project.Executors = await employees.ToListAsync(cancellationToken);
        }

        await reposManager.Projects.SaveAsync(project, cancellationToken);
        await reposManager.CommitChangesAsync(cancellationToken);
        
        return project.Id;
    }
}