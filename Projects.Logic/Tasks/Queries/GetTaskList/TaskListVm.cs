namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class TaskListVm
{
    public ICollection<TaskLookupDto> Tasks { get; set; } = [];
}