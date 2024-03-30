using GameStore.Api.Repositories;
using GameStore.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var ConnString = configuration.GetConnectionString("GameStore");
        services.AddSqlite<GameStoreContext>(ConnString);

        services.AddScoped<IGamesRepository, GamesRepository>();

        return services;
    }
}
