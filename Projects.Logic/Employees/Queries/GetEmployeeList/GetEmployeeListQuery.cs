using MediatR;

namespace Projects.Logic.Employees.Queries.GetEmployeeList;

public class GetEmployeeListQuery : IRequest<EmployeeListVm>;