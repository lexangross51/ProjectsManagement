using Projects.DataAccess.Models;

namespace Projects.Logic.Employees.Queries.GetEmployee;

public class EmployeeDetailsVm
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string MiddleName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Mail { get; init; } = string.Empty;
    
    public IEnumerable<Project>? Projects { get; init; }
    
    public IEnumerable<Project>? ManagedProjects { get; init; }
}