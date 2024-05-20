using MediatR;
using Projects.Logic.Projects.Queries.GetProjectList;

namespace Projects.Logic.Projects.Queries.SortProjects;

public class SortProjectsQuery : IRequest<ProjectListVm>
{
    public ProjectListVm ProjectListVm { get; init; } = default!; // Data to sort

    public string Column { get; init; } = string.Empty;

    public string Order { get; init; } = string.Empty;
}