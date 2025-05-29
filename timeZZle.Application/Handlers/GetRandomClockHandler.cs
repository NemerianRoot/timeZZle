using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers;

public sealed record GetRandomClockQuery : IQuery<Clock>;

internal sealed class GetRandomClockHandler(
    IClockRepository clockRepository) : IQueryHandler<GetRandomClockQuery, Clock>
{
    public async Task<Result<Clock>> Handle(GetRandomClockQuery request, CancellationToken cancellationToken)
    {
        var result = await clockRepository.GetRandom();

        return result;
    }
}