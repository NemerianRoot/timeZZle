using Microsoft.Extensions.DependencyInjection;
using timeZZle.Data.Context;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Data.Repositories;

namespace timeZZle.Data;

public static class Registry
{
    public static IServiceCollection RegisterData(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        
        services.AddScoped<IClockRepository, ClockRepository>();
        
        return services;
    }
}