using Projects.DataAccess.Models.Tasks;
using Projects.DataAccess.Storage.Base;

namespace Projects.DataAccess.Storage.TasksStorage;

public interface ITaskRepository : IRepository<ProjectTask>;