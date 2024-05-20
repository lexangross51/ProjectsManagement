using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projects.DataAccess.Database;
using Projects.DataAccess.Storage.EmployeesStorage;
using Projects.DataAccess.Storage.ProjectsStorage;
using Projects.DataAccess.Storage.UnitOfWork;

namespace Projects.DataAccess;

public static class DiExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        Action<DbContextOptionsBuilder> options)
    {
        services
            .AddScoped<IEmployeeRepository, EmployeeRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            .AddScoped<IUnitOfWork, ProjectsManager>()
            .AddDbContext<AppDbContext>(options);

        return services;
    }
}