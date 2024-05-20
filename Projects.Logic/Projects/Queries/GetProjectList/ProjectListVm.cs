namespace Projects.Logic.Projects.Queries.GetProjectList;

public class ProjectListVm
{
    public ICollection<ProjectLookupDto> Projects { get; set; } = [];
}