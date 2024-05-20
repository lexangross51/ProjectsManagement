using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database;
using Projects.DataAccess.Models;

namespace Projects.DataAccess.Storage.ProjectsStorage;

public class ProjectRepository(AppDbContext context) : IProjectRepository
{
    public async Task<Project?> GetAsync(Guid id, CancellationToken token)
        => await context.Projects.SingleOrDefaultAsync(p => p.Id == id, token);

    public async Task<IQueryable<Project>?> GetAllAsync(CancellationToken token)
        => await Task.FromResult(context.Projects);

    public async Task<Project?> GetWithExecutorsAsync(Guid id, CancellationToken token)
        => await context.Projects.Include(p => p.Executors)
            .SingleOrDefaultAsync(p => p.Id == id, token);
    
    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        var entity = await context.Projects.SingleOrDefaultAsync(p => p.Id == id, token);

        if (entity == null) return;

        context.Projects.Remove(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Project entity, CancellationToken token)
    {
        var newExecutors = entity.Executors == null ? null : new List<Employee>(entity.Executors);
        var newExecutorsId = newExecutors?.Select(e => e.Id).ToList() ?? [];
        
        var old = await context.Projects
            .Include(p => p.Executors)
            .SingleOrDefaultAsync(p => p.Id == entity.Id, token);
        
        if (old == null) return;
        
        var oldExecutors = old.Executors;
        var toDelete = oldExecutors?.Where(e => !newExecutorsId.Contains(e.Id)).ToList();
        
        // Удалить ненужных
        if (toDelete != null)
        {
            foreach (var employee in toDelete)
            {
                old.Executors?.Remove(employee);
            }
        }
        
        // Добавить новых, а тех, кто уже есть - оставить
        if (newExecutors != null)
        {
            old.Executors ??= [];
            
            foreach (var employee in newExecutors)
            {
                if (old.Executors.Any(e => e.Id == employee.Id)) continue;
                
                old.Executors.Add(employee);
            }
        }
        
        await context.SaveChangesAsync(token);
    }

    public async Task SaveAsync(Project entity, CancellationToken token)
    {
        await context.Projects.AddAsync(entity, token);
        await context.SaveChangesAsync(token);
    }
}