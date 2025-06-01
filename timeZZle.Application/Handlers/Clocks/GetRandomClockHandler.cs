using MediatR;
using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers.Clocks;

public sealed record GetRandomClockQuery : IQuery<Clock>;

internal sealed class GetRandomClockHandler(
    IClockRepository clockRepository,
    ISender sender) : IQueryHandler<GetRandomClockQuery, Clock>
{
    public async Task<Result<Clock>> Handle(GetRandomClockQuery request, CancellationToken cancellationToken)
    {
        var count = await clockRepository.Count();

        if (count == 0)
        {
            await sender.Send(new GenerateRandomClocksCommand(ClockSize: 8, BatchSize: 10), cancellationToken);
        }
        
        var result = await clockRepository.GetRandom();
        
        return result;
    }
}