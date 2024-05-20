using MediatR;

namespace Projects.Logic.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>
{
    public string ProjectName { get; set; } = string.Empty;

    public string CompanyCustomer { get; set; } = string.Empty;

    public string CompanyExecutor { get; set; } = string.Empty;
    
    public uint Priority { get; set; }
    
    public DateOnly DateStart { get; set; }
    
    public DateOnly DateEnd { get; set; }
    
    public Guid? ManagerId { get; set; }
    
    public IEnumerable<Guid>? ExecutorsId { get; set; }
}