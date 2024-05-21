using MediatR;
using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.Logic.Common.Exceptions;

namespace Projects.Logic.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler(IEmployeeRepository repos) : IRequestHandler<DeleteEmployeeCommand>
{
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        _ = await repos.GetAsync(request.Id, cancellationToken) ?? 
            throw new NotFoundException(nameof(Employee), request.Id);

        await repos.DeleteAsync(request.Id, cancellationToken);
    }
}