using timeZZle.Shared.Enums;

namespace timeZZle.Dtos.Clocks;

public record ClockDto(Guid Id, int Size, BoxDto[] Boxes, Difficulty Difficulty);