using Mapster;
using timeZZle.Application.Handlers.Puzzles;
using timeZZle.Domain.Entities;
using timeZZle.Dtos.Clocks;
using timeZZle.Dtos.Puzzles;

namespace timeZZle.Web.Api;

public class MappingProfiles : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Clock, ClockDto>();
        config.NewConfig<Box, BoxDto>();

        config.NewConfig<PropositionDto, VerifyPropositionCommand>();
        config.NewConfig<PlayerPickDto, PickInput>();
    }
}