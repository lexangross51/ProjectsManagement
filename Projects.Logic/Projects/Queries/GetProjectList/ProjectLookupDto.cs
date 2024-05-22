namespace Projects.Logic.Projects.Queries.GetProjectList;

public readonly struct ProjectLookupDto
{
    public Guid Id { get; init; }

    public string ProjectName { get; init; }
    
    public uint Priority { get; init; }
    
    public DateOnly DateStart { get; init; }
    
    public DateOnly DateEnd { get; init; }
}