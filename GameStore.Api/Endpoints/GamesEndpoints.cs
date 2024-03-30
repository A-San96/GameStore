using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using GameStore.Api.Repositories;
using GameStore.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    const string RoutePrefix = "games";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup(RoutePrefix)
                        .WithParameterValidation()
                        .WithOpenApi()
                        .WithTags("Games");


        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext, IGamesRepository gamesRepository) =>
        {
            var games = await gamesRepository.GetAllAsync();
            return games.Select(game => game.ToGameSummaryDto()).ToList();
        });

        // GET /games/1
        group.MapGet("/{id}", async (int id, IGamesRepository gamesRepository) =>
        {
            Game? game = await gamesRepository.GetAsync(id);

            return game is null ?
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, IGamesRepository gamesRepository) =>
        {
            Game game = newGame.ToEntity();

            await gamesRepository.CreateAsync(game);

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, IGamesRepository gamesRepository) =>
        {
            var existingGame = await gamesRepository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            await gamesRepository.UpdateAsync(updatedGame.ToEntity(id));

            return Results.NoContent();
        });

        // DELETE games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext, IGamesRepository gamesRepository) =>
        {
            await gamesRepository.DeleteAsync(id);

            return Results.NoContent();
        });

        return group;
    }

}
