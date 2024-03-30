using GameStore.Api.Entities;

namespace GameStore.Api.Repositories.Contracts;

public interface IGenresRepository
{
    Task<IEnumerable<Genre>> GetAllAsync();
}
