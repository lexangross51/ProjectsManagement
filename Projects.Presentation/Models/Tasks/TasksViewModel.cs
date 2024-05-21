using Projects.Logic.Tasks.Queries.FilterTasks.Filters;
using Projects.Logic.Tasks.Queries.GetTaskList;

namespace Projects.Presentation.Models.Tasks;

public class TasksViewModel
{
    public TaskListVm TaskList { get; set; } = default!;

    public PriorityFilter PriorityFilter { get; init; } = default!;

    public TaskStatusFilter StatusFilter { get; init; } = default!;

    public string SortBy { get; init; } = string.Empty;

    public string SortOrder { get; init; } = string.Empty;
    
    public Guid? ProjectId { get; init; }
    
    public bool ShowDeleteAndAddButtons { get; set; }
}