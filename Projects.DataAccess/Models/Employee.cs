using Projects.DataAccess.Models.Base;

namespace Projects.DataAccess.Models;

public class Employee : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    
    public IEnumerable<Project>? Projects { get; set; }
    
    public IEnumerable<Project>? ManagedProjects { get; set; }
}