using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.DataAccess.Storage.ProjectsStorage;

namespace Projects.DataAccess.Storage.UnitOfWork;

public interface IUnitOfWork
{
    IEmployeeRepository Employees { get; }
    
    IProjectRepository Projects { get; }

    Task CommitChangesAsync(CancellationToken token);
}