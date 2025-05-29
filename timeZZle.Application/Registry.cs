using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using timeZZle.Application.Helpers;
using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Application.Interfaces.Services;
using timeZZle.Application.Services;
using timeZZle.Application.Validators;

namespace timeZZle.Application;

public static class Registry
{
    public static IServiceCollection RegisterApp(this IServiceCollection services)
    {
        return services.RegisterInternalServices().ConfigureMediatR();
    }

    private static IServiceCollection RegisterInternalServices(this IServiceCollection services)
    {
        services.AddScoped<IClockService, ClockService>();
        services.AddScoped<IClockHelper, ClockHelper>();

        return services;
    }

    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(Registry).Assembly);
        });
        
        services.AddValidatorsFromAssembly(typeof(Registry).Assembly, includeInternalTypes: true);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        return services;
    }
}