using Mapster;
using timeZZle.Domain.Entities;
using timeZZle.Dtos.Clocks;

namespace timeZZle.Web.Api;

public class MappingProfiles : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClockDto, Clock>();
        config.NewConfig<BoxDto, Box>();
    }
}