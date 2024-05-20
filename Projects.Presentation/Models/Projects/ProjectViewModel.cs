using Projects.Logic.Projects.Queries.FilterProjects.Filters;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Presentation.Models.Projects;

public class ProjectsViewModel
{
    public ProjectListVm ProjectsList { get; set; } = default!;

    public PriorityFilter PriorityFilter { get; init; } = default!;
    
    public DateFilter DateStartFilter { get; init; } = default!;

    public DateFilter DateEndFilter { get; init; } = default!;

    public string SortBy { get; init; } = string.Empty;
    
    public string SortOrder { get; init; } = string.Empty;
}