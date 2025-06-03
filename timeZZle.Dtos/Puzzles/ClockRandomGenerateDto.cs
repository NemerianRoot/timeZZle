using System.ComponentModel;

namespace timeZZle.Dtos.Puzzles;

public record ClockRandomGenerateDto(
    [DefaultValue(08)] int ClockSize = 8, 
    [DefaultValue(10)] int BatchSize = 10);