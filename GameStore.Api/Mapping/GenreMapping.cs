using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GenreMapping
{
    public static Genre ToEntity(this GenreDto genre)
    {
        return new Genre()
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }

    public static GenreDto ToGenreDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
