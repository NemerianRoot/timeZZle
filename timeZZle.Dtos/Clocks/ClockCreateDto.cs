using timeZZle.Shared.Enums;

namespace timeZZle.Dtos.Clocks;

public record ClockCreateDto(Difficulty Difficulty, BoxCreateDto[] Boxes);