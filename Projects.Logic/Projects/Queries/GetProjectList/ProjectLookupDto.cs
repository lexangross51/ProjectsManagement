namespace Projects.Logic.Projects.Queries.GetProjectList;

public class ProjectLookupDto
{
    public Guid Id { get; init; }

    public string ProjectName { get; init; } = string.Empty;
    
    public uint Priority { get; init; }
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }
}