using timeZZle.Application.Interfaces.Helpers;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers.Clocks;

public sealed record GenerateRandomClocksCommand(int ClockSize, int BatchSize = 10) : ICommand;

internal class GenerateRandomClocksHandler(
    IClockRepository clockRepository,
    IClockHelper clockHelper) : ICommandHandler<GenerateRandomClocksCommand>
{
    public async Task<Result> Handle(GenerateRandomClocksCommand request, CancellationToken cancellationToken)
    {
        for (var i = 0; i < request.BatchSize; i++)
        {
            await clockRepository.Add(clockHelper.GenerateRandomSolvable(request.ClockSize, 6));
        }

        await clockRepository.SaveChangesAsync();

        return Result.Success();
    }
}