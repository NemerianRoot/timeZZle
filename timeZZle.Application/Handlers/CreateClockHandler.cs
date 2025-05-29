using timeZZle.Data.Contracts.Repositories;
using timeZZle.Domain.Entities;
using timeZZle.Shared.Enums;
using timeZZle.Shared.Interfaces.Messaging;
using timeZZle.Shared.Utils;

namespace timeZZle.Application.Handlers;

public sealed record BoxInput(int Position, int Value);

public sealed record CreateClockCommand(Difficulty Difficulty, BoxInput[] Boxes)
    : ICommand<Clock>;

internal sealed class CreateClockHandler(
    IClockRepository clockRepository) : ICommandHandler<CreateClockCommand, Clock>
{
    public async Task<Result<Clock>> Handle(CreateClockCommand request, CancellationToken cancellationToken)
    {
        var clock = new Clock
        {
            Size = request.Boxes.Length,
            Difficulty = request.Difficulty,
            Boxes = request.Boxes.Select(Map).ToArray()
        };

        await clockRepository.Add(clock);
        await clockRepository.SaveChangesAsync();

        return clock;
    }

    private static Box Map(BoxInput boxInput)
    {
        return new Box
        {
            Position = boxInput.Position,
            Value = boxInput.Value
        };
    }
}