using MediatR;
using Projects.DataAccess.Storage.EmployeesStorage;

namespace Projects.Logic.Employees.Queries.GetEmployeeList;

public class GetEmployeeListQueryHandler(IEmployeeRepository repos)
    : IRequestHandler<GetEmployeeListQuery, EmployeeListVm>
{
    public async Task<EmployeeListVm> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        var allEmployees = await repos.GetAllAsync(cancellationToken);
        var listVm = new EmployeeListVm();

        if (allEmployees == null) return listVm;
        
        foreach (var employee in allEmployees)
        {
            listVm.Employees.Add(new EmployeeLookupDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName
            });
        }

        return listVm;
    }
}