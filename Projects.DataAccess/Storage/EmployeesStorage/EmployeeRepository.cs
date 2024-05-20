using Microsoft.EntityFrameworkCore;
using Projects.DataAccess.Database;
using Projects.DataAccess.Models;

namespace Projects.DataAccess.Storage.EmployeesStorage;

public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
{
    public async Task<Employee?> GetAsync(Guid id, CancellationToken token)
        => await context.Employees.SingleOrDefaultAsync(e => e.Id == id, token);

    public async Task<IQueryable<Employee>?> GetAllAsync(CancellationToken token)
        => await Task.FromResult(context.Employees);
    
    public async Task<Employee?> GetWithProjectsAsync(Guid id, CancellationToken token)
        => await context.Employees
            .Include(e => e.Projects)
            .Include(e => e.ManagedProjects)
            .SingleOrDefaultAsync(e => e.Id == id, token);

    public async Task DeleteAsync(Guid id, CancellationToken token)
    {
        var entity = await context.Employees.SingleOrDefaultAsync(e => e.Id == id, token);

        if (entity == null) return;

        context.Employees.Remove(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(Employee entity, CancellationToken token)
    {
        context.Employees.Update(entity);
        await context.SaveChangesAsync(token);
    }

    public async Task SaveAsync(Employee entity, CancellationToken token)
    {
        await context.Employees.AddAsync(entity, token);
        await context.SaveChangesAsync(token);
    }
}