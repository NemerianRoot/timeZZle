using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace timeZZle.Application;

public static class Registry
{
    public static IServiceCollection RegisterApp(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(Registry).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(Registry).Assembly, includeInternalTypes: true);
        
        return services;
    }
}