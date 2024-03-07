using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    const string RoutePrefix = "genres";
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(RoutePrefix)
                        .WithParameterValidation()
                        .WithOpenApi()
                        .WithTags("Genres");

        // GET /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Genres
                    .Select(genre => genre.ToGenreDto())
                    .AsNoTracking()
                    .ToListAsync());

        return group;
    }

}
