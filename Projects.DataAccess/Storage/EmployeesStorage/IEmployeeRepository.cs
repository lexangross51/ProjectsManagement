using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.Base;

namespace Projects.DataAccess.Storage.EmployeesStorage;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetWithProjectsAsync(Guid id, CancellationToken token);
}