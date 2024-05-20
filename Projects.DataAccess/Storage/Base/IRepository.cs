using Projects.DataAccess.Models.Base;

namespace Projects.DataAccess.Storage.Base;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetAsync(Guid id, CancellationToken token);

    Task<IQueryable<TEntity>?> GetAllAsync(CancellationToken token);

    Task DeleteAsync(Guid id, CancellationToken token);

    Task UpdateAsync(TEntity entity, CancellationToken token);

    Task SaveAsync(TEntity entity, CancellationToken token);
}