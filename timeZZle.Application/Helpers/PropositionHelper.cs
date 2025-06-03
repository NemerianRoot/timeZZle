using Microsoft.Extensions.DependencyInjection;
using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Comparers;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Helpers;

[Registry(typeof(IPropositionHelper), ServiceLifetime.Scoped)]
internal class PropositionHelper(
    IClockHelper clockHelper) : IPropositionHelper
{
    public bool TrySolution(Clock clock, IEnumerable<Guid> sortedBoxIds)
    {
        var boxIdByPosition = clock.Boxes!.ToDictionary(o => o.Id, o => o.Position as int?);
        var proposition = sortedBoxIds.Select(o => boxIdByPosition.GetValueOrDefault(o, -1)).ToArray();

        var possibleSolutions = clockHelper.Solve(clock);

        return possibleSolutions.Contains(proposition, new PropositionEqualityComparer());
    }
}