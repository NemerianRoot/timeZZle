using Mapster;
using timeZZle.Domain.Entities;
using timeZZle.Dtos.Clocks;

namespace timeZZle.Web.Api;

public static class MappingProfiles
{
    public static void RegisterClocksMapping()
    {
        TypeAdapterConfig<ClockCreateDto, Clock>.NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Size);

        TypeAdapterConfig<BoxCreateDto, Box>.NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.Clock)
            .Ignore(dest => dest.ClockId);

        TypeAdapterConfig<ClockDto, Clock>.NewConfig();
        TypeAdapterConfig<BoxDto, Box>.NewConfig();
    }
}