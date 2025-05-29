using Mapster;
using MapsterMapper;
using timeZZle.Web.Api;

namespace timeZZle.Extensions;

public static class MapsterRegistrations
{
    public static IServiceCollection RegisterMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MappingProfiles).Assembly);

        services.AddSingleton(config);  
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}