using timeZZle.Domain.Entities;

namespace timeZZle.Application.Interfaces.Helpers;

internal interface IClockHelper
{
    List<int?[]> Solve(Clock clock);

    bool IsSolvable(Clock clock);
}