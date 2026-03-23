using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Abstraction.Services;
using Portfolio.Application.Abstraction.Validator;
using Portfolio.Application.DTO.Request;
using Portfolio.Application.Services.Technology.Validator;

namespace Portfolio.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITechnologyService, ITechnologyService>();

        services.AddScoped<IValidate<int>, ValidateTechnologyID>();
        services.AddScoped<IValidate<TechnologyCreateRequestDto>, ValidateCreateTechnology>();
        services.AddScoped<IValidate<TechnologyUpdateRequestDto>, ValidateUpdateTechnology>();

        return services;
    }
}
