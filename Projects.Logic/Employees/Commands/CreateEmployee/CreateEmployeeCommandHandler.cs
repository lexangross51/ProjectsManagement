using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.EmployeesStorage;

namespace Projects.Logic.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler(IEmployeeRepository repos) : IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            Id = request.Id,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Mail = request.Mail
        };

        await repos.SaveAsync(employee, cancellationToken);
        return employee.Id;
    }
}