using MediatR;

namespace Projects.Logic.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    
    public string FirstName { get; init; } = string.Empty;

    public string MiddleName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Mail { get; init; } = string.Empty;
}