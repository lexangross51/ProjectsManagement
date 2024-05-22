using MediatR;

namespace Projects.Logic.Projects.Queries.GetProjectList;

public class GetProjectListQuery : IRequest<ProjectListVm>
{
    public Guid UserId { get; set; }

    public string Role { get; set; } = string.Empty;
}