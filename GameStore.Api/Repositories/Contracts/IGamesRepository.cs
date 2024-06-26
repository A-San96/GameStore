﻿using GameStore.Api.Entities;

namespace GameStore.Api.Repositories.Contracts;

public interface IGamesRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetAsync(int id);
    Task CreateAsync(Game game);
    Task UpdateAsync(Game updatedGame);
    Task DeleteAsync(int id);

}
