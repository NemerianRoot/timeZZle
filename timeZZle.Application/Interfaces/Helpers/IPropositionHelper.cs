using timeZZle.Domain.Entities;

namespace timeZZle.Application.Interfaces.Helpers;

internal interface IPropositionHelper
{
    bool TrySolution(Clock clock, IEnumerable<Guid> sortedBoxIds);
}
