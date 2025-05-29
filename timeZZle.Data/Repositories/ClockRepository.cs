using Microsoft.EntityFrameworkCore;
using timeZZle.Data.Base;
using timeZZle.Data.Context;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Enums;

namespace timeZZle.Data.Repositories;

public class ClockRepository(AppDbContext context) : RepositoryBase<Clock>(context), IClockRepository
{
    public async Task<Clock?> FindAsync(Guid id)
    {
        return await Context.Set<Clock>().Include(o => o.Boxes).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Clock> GetRandom(Difficulty? difficulty = null)
    {
        return await Context.Set<Clock>().OrderBy(o => Guid.NewGuid()).Include(o => o.Boxes)
            .Where(o => !difficulty.HasValue || o.Difficulty == difficulty).FirstAsync();
    }
}