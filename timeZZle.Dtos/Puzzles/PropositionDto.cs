namespace timeZZle.Dtos.Puzzles;

public record PlayerPickDto(Guid BoxId, int PlayedAt);

public record PropositionDto(Guid ClockId, PlayerPickDto[] PlayerPicks);