using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database;
using Projects.DataAccess.Models;
using Projects.DataAccess.Models.Tasks;

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
        // Executors
        var newExecutors = entity.Executors == null ? null : new List<Employee>(entity.Executors);
        var newExecutorsId = newExecutors?.Select(e => e.Id).ToList() ?? [];

        var old = await context.Projects
            .Include(p => p.Executors)
            .Include(project => project.Tasks)
            .SingleOrDefaultAsync(p => p.Id == entity.Id, token);

        if (old == null) return;

        var oldExecutors = old.Executors;
        var employeesToDelete = oldExecutors?.Where(e => !newExecutorsId.Contains(e.Id)).ToList();

        // Delete the unwanted
        if (employeesToDelete != null)
        {
            foreach (var employee in employeesToDelete)
            {
                old.Executors?.Remove(employee);
            }
        }

        // Add new ones, and keep the ones already have
        if (newExecutors != null)
        {
            old.Executors ??= [];

            foreach (var employee in newExecutors)
            {
                if (old.Executors.Any(e => e.Id == employee.Id)) continue;

                old.Executors.Add(employee);
            }
        }

        // Tasks
        var newTasks = entity.Tasks == null ? null : new List<ProjectTask>(entity.Tasks);
        var newTasksId = newTasks?.Select(e => e.Id).ToList() ?? [];

        var oldTasks = old.Tasks;
        var tasksToDelete = oldTasks?.Where(t => !newTasksId.Contains(t.Id)).ToList();

        // Delete the unwanted
        if (tasksToDelete != null)
        {
            foreach (var task in tasksToDelete)
            {
                old.Tasks?.Remove(task);
            }
        }

        // Add new ones, and keep the ones already have
        if (newTasks != null)
        {
            old.Tasks ??= [];

            foreach (var task in newTasks)
            {
                if (old.Tasks.Any(e => e.Id == task.Id)) continue;

                old.Tasks.Add(task);
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