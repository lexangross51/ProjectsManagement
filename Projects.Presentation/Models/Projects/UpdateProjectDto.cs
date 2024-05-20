using System.ComponentModel.DataAnnotations;
using Projects.DataAccess.Models;

namespace Projects.Presentation.Models.Projects;

public class UpdateProjectDto
{
    public Guid Id { get; init; }
    
    [MaxLength(256)]
    public string ProjectName { get; init; } = default!;
    
    [MaxLength(256)]
    public string CompanyCustomer { get; init; } = default!;

    [MaxLength(256)]
    public string CompanyExecutor { get; init; } = default!;
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }
    
    [Range(0, 10)]
    public uint Priority { get; init; }

    public Guid? ManagerId { get; init; }

    public Employee? Manager { get; init; }

    public string? ExecutorsId { get; init; }
    
    public ICollection<Employee>? Executors { get; init; }
}