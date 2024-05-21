using System.ComponentModel.DataAnnotations;
using Projects.DataAccess.Models;
using TaskStatus = Projects.DataAccess.Models.Tasks.TaskStatus;

namespace Projects.Presentation.Models.Tasks;

public class UpdateTaskDto
{
    public Guid Id { get; init; }

    [MaxLength(256)]
    public string TaskName { get; init; } = string.Empty;

    [Range(0, 10)]
    public uint Priority { get; init; }

    public TaskStatus Status { get; init; }

    [MaxLength(512)]
    public string? Description { get; init; }

    public Guid? ExecutorId { get; init; }
    
    public Employee? Executor { get; init; } 
}