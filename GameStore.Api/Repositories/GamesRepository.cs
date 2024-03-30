using GameStore.Api.Data;
using GameStore.Api.Entities;
using GameStore.Api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class GamesRepository : IGamesRepository
{
    private readonly GameStoreContext _dbContext;

    public GamesRepository(GameStoreContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(Game game)
    {
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await _dbContext.Games
                        .Include(game => game.Genre)
                        .AsNoTracking()
                        .ToListAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await _dbContext.Games
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id);

    }

    public async Task UpdateAsync(Game updatedGame)
    {
        _dbContext.Update(updatedGame);
        await _dbContext.SaveChangesAsync();
    }
}
