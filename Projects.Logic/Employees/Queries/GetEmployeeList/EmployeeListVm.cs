namespace Projects.Logic.Employees.Queries.GetEmployeeList;

public class EmployeeListVm
{
    public ICollection<EmployeeLookupDto> Employees { get; } = [];
}