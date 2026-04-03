using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.Common.Validator;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.Services.Blog;
using Portfolio.Application.Services.Blog.Validator;
using Portfolio.Application.Services.Project;
using Portfolio.Application.Services.Project.Validator;
using Portfolio.Application.Services.Technology;
using Portfolio.Application.Services.Technology.Validator;
using Portfolio.Application.Services.Website;
using Portfolio.Application.Services.Website.Validator;

namespace Portfolio.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITechnologyService, TechnologyService>();
        services.AddScoped<IWebsiteService, WebsiteService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IProjectService, ProjectService>();

        services.AddScoped<IValidate<int>, ValidateID>();
        services.AddScoped<IValidate<ChangeNameRequestDto>, ValidateChangeName>();
        services.AddScoped<IValidate<ChangePasswordRequestDto>, ValidateChangePassword>();
        services.AddScoped<IValidate<TechnologyRequestDto>, ValidateCreateTechnology>();
        services.AddScoped<IValidate<TechnologyUpdateRequestDto>, ValidateUpdateTechnology>();
        services.AddScoped<IValidate<ProjectUpdateRequestDto>, ValidateUpdateProject>();
        services.AddScoped<IValidate<ProjectRequestDto>, ValidateCreateProject>();
        services.AddScoped<IValidate<BlogRequestDto>, ValidateCreateBlog>();
        services.AddScoped<IValidate<BlogUpdateRequestDto>, ValidateUpdateBlog>();
        services.AddScoped<IValidate<WebsiteConfigUpdateRequestDto>, ValidateUpdateWebsiteConfig>();

        return services;
    }
}
