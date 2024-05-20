using MediatR;

namespace Projects.Logic.Employees.Queries.GetEmployee;

public class GetEmployeeQuery : IRequest<EmployeeDetailsVm>
{
    public Guid Id { get; init; }
}