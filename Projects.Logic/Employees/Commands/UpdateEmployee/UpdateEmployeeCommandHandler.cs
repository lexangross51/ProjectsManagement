using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler(IEmployeeRepository repos) : IRequestHandler<UpdateEmployeeCommand>
{
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await repos.GetAsync(request.Id, cancellationToken) ??
                       throw new NotFoundException(nameof(Employee), request.Id);

        employee.FirstName = request.FirstName;
        employee.MiddleName = request.MiddleName;
        employee.LastName = request.LastName;
        employee.Mail = request.Mail;

        await repos.UpdateAsync(employee, cancellationToken);
    }
}