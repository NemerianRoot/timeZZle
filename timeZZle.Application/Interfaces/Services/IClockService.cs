namespace timeZZle.Application.Interfaces.Services;

internal interface IClockService
{
    Task<bool> TrySolution(Guid clockId, IEnumerable<Guid> sortedBoxIds);
}