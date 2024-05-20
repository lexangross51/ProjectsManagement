using MediatR;

namespace Projects.Logic.Projects.Commands.UpdateProject;

public class UpdateProjectCommand : IRequest
{
    public Guid Id { get; init; }

    public string ProjectName { get; init; } = string.Empty;

    public string CompanyCustomer { get; init; } = string.Empty;

    public string CompanyExecutor { get; init; } = string.Empty;
    
    public uint Priority { get; init; }
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }
    
    public Guid? ManagerId { get; init; }
    
    public Guid? Manager { get; init; }
    
    public IEnumerable<Guid>? ExecutorsId { get; init; }
}