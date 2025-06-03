using Microsoft.Extensions.DependencyInjection;
using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Application.Interfaces.Services;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Comparers;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Services;

[Registry(typeof(IClockService), ServiceLifetime.Scoped)]
internal class ClockService(
    IClockRepository repository,
    IClockHelper clockHelper) : IClockService
{
    public Clock GetDummy()
    {
        var clock = new Clock { Size = 5 };

        var box0 = new Box { Clock = clock, Position = 0, Value = 2 };
        var box1 = new Box { Clock = clock, Position = 1, Value = 1 };
        var box2 = new Box { Clock = clock, Position = 2, Value = 1 };
        var box3 = new Box { Clock = clock, Position = 3, Value = 2 };
        var box4 = new Box { Clock = clock, Position = 4, Value = 2 };
        clock.Boxes = [box0, box1, box2, box3, box4];

        return clock;
    }

    public async Task<bool> TrySolution(Guid clockId, IEnumerable<Guid> sortedBoxIds)
    {
        var clock = await repository.FindAsync(clockId);

        if (clock == null)
        {
            return false;
        }

        var boxIdByPosition = clock.Boxes.ToDictionary(o => o.Id, o => o.Position as int?);
        var proposition = sortedBoxIds.Select(o => boxIdByPosition.GetValueOrDefault(o, -1)).ToArray();

        return TrySolution(clock, proposition);
    }

    public async Task<bool> TrySolution(Guid clockId, int?[] proposition)
    {
        var clock = await repository.FindAsync(clockId);

        return clock != null && TrySolution(clock, proposition);
    }

    private bool TrySolution(Clock clock, int?[] proposition)
    {
        var solutions = clockHelper.Solve(clock);

        return solutions.Contains(proposition, new PropositionEqualityComparer());
    }
}