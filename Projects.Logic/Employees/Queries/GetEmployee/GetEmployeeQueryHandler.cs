using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Employees.Queries.GetEmployee;

public class GetEmployeeQueryHandler(IEmployeeRepository repos) : IRequestHandler<GetEmployeeQuery, EmployeeDetailsVm>
{
    public async Task<EmployeeDetailsVm> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employee = await repos.GetWithProjectsAsync(request.Id, cancellationToken);

        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        return new EmployeeDetailsVm
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            LastName = employee.LastName,
            Mail = employee.Mail,
            Projects = employee.Projects,
            ManagedProjects = employee.ManagedProjects
        };
    }
}