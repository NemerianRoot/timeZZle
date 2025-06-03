using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers.Puzzles;

public sealed record PickInput(Guid BoxId, int PlayedAt);

public sealed record VerifyPropositionCommand(Guid ClockId, PickInput[] PlayerPicks) : ICommand<bool>;

internal class VerifyPropositionHandler(
    IClockRepository clockRepository,
    IPropositionHelper propositionHelper) : ICommandHandler<VerifyPropositionCommand, bool>
{
    public async Task<Result<bool>> Handle(VerifyPropositionCommand request, CancellationToken cancellationToken)
    {
        var clock = await clockRepository.FindAsync(request.ClockId);

        var sortedBoxIds = request.PlayerPicks.OrderBy(o => o.PlayedAt).Select(o => o.BoxId);

        return propositionHelper.TrySolution(clock, sortedBoxIds);
    }
}