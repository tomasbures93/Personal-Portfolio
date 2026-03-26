using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Infrastructure.Persistence;

namespace Portfolio.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IWebsiteRepository, WebsiteRepository>();

        return services;
    }
}
