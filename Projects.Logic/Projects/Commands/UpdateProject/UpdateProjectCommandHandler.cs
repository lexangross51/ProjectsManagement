using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.UnitOfWork;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler(IUnitOfWork reposManager) : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await reposManager.Projects.GetAsync(request.Id, cancellationToken);

        if (project == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        project.ProjectName = request.ProjectName;
        project.Priority = request.Priority;
        project.CompanyCustomer = request.CompanyCustomer;
        project.CompanyExecutor = request.CompanyExecutor;
        project.DateStart = request.DateStart;
        project.DateEnd = request.DateEnd;
        project.ManagerId = request.ManagerId;

        if (request.ExecutorsId != null)
        {
            var employees = await reposManager.Employees.GetAllAsync(cancellationToken);
            employees = employees?.Where(e => request.ExecutorsId.Contains(e.Id));

            if (employees != null)
            {
                project.Executors = await employees.ToListAsync(cancellationToken);
            }
        }

        await reposManager.Projects.UpdateAsync(project, cancellationToken);
        await reposManager.CommitChangesAsync(cancellationToken);
    }
}