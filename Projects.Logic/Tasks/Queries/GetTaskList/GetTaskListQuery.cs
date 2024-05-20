using MediatR;

namespace Projects.Logic.Tasks.Queries.GetTaskList;

public class GetTaskListQuery : IRequest<TaskListVm>;