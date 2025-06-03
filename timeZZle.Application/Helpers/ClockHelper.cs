using Microsoft.Extensions.DependencyInjection;
using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Helpers;

[Registry(typeof(IClockHelper), ServiceLifetime.Scoped)]
internal class ClockHelper : IClockHelper
{
    public Clock GenerateRandomSolvable(int size, int maxValue)
    {
        var result = new Clock { Size = size };
        var boxes = new Box[size];
        var isSolvable = false;

        while (!isSolvable)
        {
            for (var i = 0; i < size; i++)
            {
                var value = Random.Shared.Next(1, maxValue);
                boxes[i] = new Box { Position = i, Value = value, Clock = result};
            }

            result.Boxes = boxes;
            isSolvable = IsSolvable(result);
        }

        return result;
    }

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

    private static void Init(ICollection<int?[]> result, Clock clock)
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
}