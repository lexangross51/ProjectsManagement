using MediatR;

namespace Projects.Logic.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string MiddleName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Mail { get; init; } = string.Empty;
}