using timeZZle.Data.Context;

namespace timeZZle.Extensions;

public static class DatabaseExtensions
{
    public static void Seed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
}