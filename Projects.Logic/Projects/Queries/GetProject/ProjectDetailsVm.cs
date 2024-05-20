using Projects.DataAccess.Models;

namespace Projects.Logic.Projects.Queries.GetProject;

public class ProjectDetailsVm
{
    public Guid Id { get; init; }

    public string ProjectName { get; set; } = string.Empty;

    public string CompanyCustomer { get; set; } = string.Empty;

    public string CompanyExecutor { get; set; } = string.Empty;
    
    public uint Priority { get; set; }
    
    public DateOnly DateStart { get; set; }
    
    public DateOnly DateEnd { get; set; }
    
    public Guid? ManagerId { get; set; }
    
    public Employee? Manager { get; set; }
    
    public ICollection<Employee>? Executors { get; set; }
}