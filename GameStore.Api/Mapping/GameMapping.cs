using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        GameSummaryDto gameSummaryDto = new(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );

        return gameSummaryDto;
    }


    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        GameDetailsDto gameDetailsDto = new(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );

        return gameDetailsDto;
    }
}
