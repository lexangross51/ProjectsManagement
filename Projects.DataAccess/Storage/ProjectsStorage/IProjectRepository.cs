using Projects.DataAccess.Models;
using Projects.DataAccess.Storage.Base;

namespace Projects.DataAccess.Storage.ProjectsStorage;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetWithExecutorsAsync(Guid id, CancellationToken token);
}