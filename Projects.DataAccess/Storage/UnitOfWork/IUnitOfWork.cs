using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.DataAccess.Storage.ProjectsStorage;
using Projects.DataAccess.Storage.TasksStorage;

namespace Projects.DataAccess.Storage.UnitOfWork;

public interface IUnitOfWork
{
    IEmployeeRepository Employees { get; }
    
    IProjectRepository Projects { get; }
    
    ITaskRepository Tasks { get; }

    Task CommitChangesAsync(CancellationToken token);
}