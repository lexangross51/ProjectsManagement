using Microsoft.Extensions.DependencyInjection;

namespace Projects.Logic;

public static class DiExtensions
{
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        var assembly = typeof(DiExtensions).Assembly;
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        // services.AddValidatorsFromAssembly(assembly);
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}