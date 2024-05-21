using Projects.DataAccess.Models.Base;
using Projects.DataAccess.Models.Tasks;

namespace Projects.DataAccess.Models;

public class Project : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();

    public string ProjectName { get; set; } = string.Empty;

    public string CompanyCustomer { get; set; } = string.Empty;

    public string CompanyExecutor { get; set; } = string.Empty;
    
    public uint Priority { get; set; }
    
    public DateOnly DateStart { get; set; }
    
    public DateOnly DateEnd { get; set; }
    
    public Guid? ManagerId { get; set; }
    
    public Employee? Manager { get; set; }
    
    public ICollection<Employee>? Executors { get; set; }

    public ICollection<ProjectTask> Tasks { get; set; } = [];
}