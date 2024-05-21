using Projects.DataAccess.Models.Base;
using Projects.DataAccess.Models.Tasks;

namespace Projects.DataAccess.Models;

public class Employee : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string FirstName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {MiddleName} {LastName}";
    
    /// <summary>
    /// Projects in which the employee is the executor
    /// </summary>
    public IEnumerable<Project>? Projects { get; init; }

    /// <summary>
    /// Projects in which the employee is a manager
    /// </summary>
    public IEnumerable<Project>? ManagedProjects { get; init; }

    /// <summary>
    /// Tasks created by this employee
    /// </summary>
    public IEnumerable<ProjectTask>? CreatedTasks { get; init; }

    /// <summary>
    /// Tasks that have been assigned to the employee in question
    /// </summary>
    public IEnumerable<ProjectTask>? Tasks { get; init; } = new List<ProjectTask>();
}