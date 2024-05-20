using Projects.DataAccess.Database;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.DataAccess.Storage.ProjectsStorage;
using Projects.DataAccess.Storage.TasksStorage;

namespace Projects.DataAccess.Storage.UnitOfWork;

public class ProjectsManager(AppDbContext context,
    IEmployeeRepository employeeRepos,
    IProjectRepository projectRepos,
    ITaskRepository taskRepos) : IUnitOfWork
{
    public IEmployeeRepository Employees { get; } = employeeRepos;

    public IProjectRepository Projects { get; } = projectRepos;

    public ITaskRepository Tasks { get; } = taskRepos;

    public async Task CommitChangesAsync(CancellationToken token)
        => await context.SaveChangesAsync(token);
}