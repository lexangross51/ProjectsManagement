using Microsoft.Extensions.DependencyInjection;

namespace Projects.Logic;

public static class DiExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DiExtensions).Assembly));
        return services;
    }
}