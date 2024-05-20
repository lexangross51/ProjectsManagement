using Projects.DataAccess.Database;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.DataAccess.Storage.ProjectsStorage;

namespace Projects.DataAccess.Storage.UnitOfWork;

public class ProjectsManager(AppDbContext context,
    IEmployeeRepository employeeRepos,
    IProjectRepository projectRepos) : IUnitOfWork
{
    public IEmployeeRepository Employees { get; } = employeeRepos;

    public IProjectRepository Projects { get; } = projectRepos;

    public async Task CommitChangesAsync(CancellationToken token)
        => await context.SaveChangesAsync(token);
}