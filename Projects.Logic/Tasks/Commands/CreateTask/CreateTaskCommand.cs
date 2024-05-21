using MediatR;

namespace Projects.Logic.Tasks.Commands.CreateTask;

public class CreateTaskCommand : IRequest<Guid>
{
    public string TaskName { get; init; } = string.Empty;

    public uint Priority { get; init; }

    public string? Description { get; init; }

    public Guid AuthorId { get; init; }

    public Guid? ExecutorId { get; init; }

    public Guid ProjectId { get; init; }
}