using MediatR;

namespace Projects.Logic.Projects.Queries.GetProjectList;

public class GetProjectListQuery : IRequest<ProjectListVm>
{
    public Guid UserId { get; init; }

    public string Role { get; set; } = string.Empty;
}