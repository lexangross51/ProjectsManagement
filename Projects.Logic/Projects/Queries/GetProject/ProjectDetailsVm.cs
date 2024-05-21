using Projects.DataAccess.Models;
using Projects.DataAccess.Models.Tasks;

namespace Projects.Logic.Projects.Queries.GetProject;

public class ProjectDetailsVm
{
    public Guid Id { get; init; }

    public string ProjectName { get; init; } = string.Empty;

    public string CompanyCustomer { get; init; } = string.Empty;

    public string CompanyExecutor { get; init; } = string.Empty;
    
    public uint Priority { get; init; }
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }
    
    public Guid? ManagerId { get; init; }
    
    public Employee? Manager { get; init; }
    
    public ICollection<Employee>? Executors { get; init; }

    public ICollection<ProjectTask>? Tasks { get; init; }
}