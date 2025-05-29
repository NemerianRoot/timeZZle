using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Comparers;

namespace timeZZle.Application.Services;

public class ClockService(IClockRepository repository)
{
    public bool IsSolvable(Clock clock)
    {
        var result = Solve(clock);

        return result.Any();
    }

    public List<int?[]> Solve(Clock clock)
    {
        var result = new List<int?[]>();
        var initialize = new List<int?[]>();

        Init(initialize, clock);

        foreach (var array in initialize)
        {
            AddPossibilities(result, array, clock);
        }

        result = result.Where(o => !o.Contains(-1)).ToList();

        return result;
    }

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

    private static void AddPossibilities(ICollection<int?[]> result, int?[] array, Clock clock)
    {
        result.Add(array);
        var lastPositionDefined = array.LastOrDefault(o => o != -1);
        if (!lastPositionDefined.HasValue)
        {
            return;
        }

        var nextValueIndex = Array.IndexOf(array, -1);
        if (nextValueIndex == -1)
        {
            return;
        }

        var matchingBox = clock.Boxes.First(o => o.Position == lastPositionDefined);
        var nextPossibilities = matchingBox.GetNextPossibilities();
        var opt1 = nextPossibilities.First();
        var opt2 = nextPossibilities.Last();

        if (!array.Contains(opt1.Position))
        {
            var cloneForOpt1 = (int?[])array.Clone();
            cloneForOpt1[nextValueIndex] = opt1.Position;
            AddPossibilities(result, cloneForOpt1, clock);
        }

        if (opt1 == opt2 || array.Contains(opt2.Position))
        {
            return;
        }

        var cloneForOpt2 = (int?[])array.Clone();
        cloneForOpt2[nextValueIndex] = opt2.Position;
        AddPossibilities(result, cloneForOpt2, clock);
    }

    private void Init(ICollection<int?[]> result, Clock clock)
    {
        var arrayPattern = GetArrayOfSize(clock.Size);
        for (var i = 0; i < clock.Size; i++)
        {
            var clone = (int?[])arrayPattern.Clone();
            clone[0] = i;
            result.Add(clone);
        }
    }

    private static int?[] GetArrayOfSize(int clockSize)
    {
        var result = new List<int?>();

        for (var i = 0; i < clockSize; i++)
        {
            result.Add(-1);
        }

        return result.ToArray();
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
        var solutions = Solve(clock);

        return solutions.Contains(proposition, new PropositionEqualityComparer());
    }
}