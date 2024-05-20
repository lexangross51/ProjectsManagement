namespace Projects.Logic.Employees.Queries.GetEmployeeList;

public readonly struct EmployeeLookupDto
{
    public Guid Id { get; init; }
    
    public string FirstName { private get; init; }
    
    public string MiddleName { private get; init; }
    
    public string LastName { private get; init; }
    
    public string FullName => $"{FirstName} {MiddleName} {LastName}";
}