using timeZZle.Domain.Entities;
using timeZZle.Shared.Enums;

namespace timeZZle.Data.Contracts.Repositories;

public interface IClockRepository : IGenericRepository<Clock>
{
    Task<Clock?> FindAsync(Guid id);
    Task<Clock> GetRandom(Difficulty? difficulty = null);
}