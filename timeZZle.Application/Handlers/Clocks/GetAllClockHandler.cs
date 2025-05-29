using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers.Clocks;

public sealed record GetAllClocksQuery : IQuery<Clock[]>;

internal sealed class GetAllClockHandler(
    IClockRepository clockRepository) : IQueryHandler<GetAllClocksQuery, Clock[]>
{
    public async Task<Result<Clock[]>> Handle(GetAllClocksQuery request, CancellationToken cancellationToken)
    {
        var result = await clockRepository.GetAll(null,nameof(Clock.Boxes));

        return result.ToArray();
    }
}