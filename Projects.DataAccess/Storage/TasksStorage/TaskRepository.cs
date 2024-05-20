using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database;
using Projects.DataAccess.Models.Tasks;

namespace Projects.DataAccess.Storage.TasksStorage;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<ProjectTask?> GetAsync(Guid id, CancellationToken token)
        => await context.Tasks.SingleOrDefaultAsync(t => t.Id == id, token);

    public async Task<IQueryable<ProjectTask>?> GetAllAsync(CancellationToken token)
        => await Task.FromResult(context.Tasks);

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        var task = await context.Tasks.SingleOrDefaultAsync(t => t.Id == id, token);

        if (task == null) return;

        context.Tasks.Remove(task);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(ProjectTask entity, CancellationToken token)
    {
        context.Tasks.Update(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task SaveAsync(ProjectTask entity, CancellationToken token)
    {
        await context.Tasks.AddAsync(entity, token);
        await context.SaveChangesAsync(token);
    }
}