using System.ComponentModel.DataAnnotations;

namespace Projects.Presentation.Models.Tasks;

public class CreateTaskDto
{
    [MaxLength(256)]
    public string TaskName { get; init; } = string.Empty;

    [Range(0, 10)]
    public uint Priority { get; init; }

    [MaxLength(512)]
    public string? Description { get; init; }

    public Guid AuthorId { get; init; }

    public Guid? ExecutorId { get; init; }
    
    public Guid ProjectId { get; init; }
}