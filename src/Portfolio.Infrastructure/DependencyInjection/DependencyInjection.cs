using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Abstraction.Persistence;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Services;

namespace Portfolio.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IWebsiteRepository, WebsiteRepository>();
        services.AddScoped<IPasswordHasher, AspNetPasswordHasher>();

        return services;
    }
}
