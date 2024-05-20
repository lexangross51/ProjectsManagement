using MediatR;

namespace Projects.Logic.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest
{
    public Guid Id { get; init; }
}