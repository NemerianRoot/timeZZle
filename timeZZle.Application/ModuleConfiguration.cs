using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using timeZZle.Application.Validators;
using timeZZle.Shared.Utils;

namespace timeZZle.Application;

public static class ModuleConfiguration
{
    public static IServiceCollection RegisterApp(this IServiceCollection services)
    {
        return services
            .RegisterInternalServices()
            .ConfigureMediatR();
    }

    private static IServiceCollection RegisterInternalServices(this IServiceCollection services)
    {
        var assembly = typeof(ModuleConfiguration).Assembly;

        var registeredTypes = assembly.GetTypes().Where(o => o.GetCustomAttribute<RegistryAttribute>() != null);
        
        foreach (var type in registeredTypes)
        {
            var attributeInstance = type.GetCustomAttribute<RegistryAttribute>();

            if (attributeInstance == null)
            {
                throw new InvalidOperationException("WTF??!?");
            }
            
            services.Add((new ServiceDescriptor(attributeInstance.InterfaceType, type, attributeInstance.ServiceLifetime)));
        }
        
        return services;
    }

    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(typeof(ModuleConfiguration).Assembly);
        });
        
        services.AddValidatorsFromAssembly(typeof(ModuleConfiguration).Assembly, includeInternalTypes: true);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        return services;
    }
}