using MediatR;
using Projects.DataAccess.Models;
using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Logic.Tasks.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest
{
    public Guid Id { get; init; }

    public string TaskName { get; init; } = string.Empty;

    public uint Priority { get; init; }

    public string? Description { get; init; }

    public TaskStatus Status { get; init; }

    public Guid? ExecutorId { get; init; }

    public Employee? Executor { get; init; }
}