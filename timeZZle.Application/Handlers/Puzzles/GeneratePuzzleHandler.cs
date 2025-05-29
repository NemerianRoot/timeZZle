using timeZZle.Data.Contracts.Repositories;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers.Puzzles;

public sealed record GeneratePuzzleCommand(int ClockSize, int BatchSize = 10) : ICommand;

internal class GeneratePuzzleHandler(
    IClockRepository clockRepository) : ICommandHandler<GeneratePuzzleCommand>
{
    public Task<Result> Handle(GeneratePuzzleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}