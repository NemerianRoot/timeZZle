using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using timeZZle.Application.Handlers;
using timeZZle.Domain.Entities;
using timeZZle.Dtos.Clocks;
using timeZZle.Web.Api.Interfaces;
using timeZZle.Web.Api.Utils;

namespace timeZZle.Web.Api.Endpoints.Clocks;

public class CreateClockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("clocks", async (ClockCreateDto createDto, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateClockCommand(createDto.Difficulty, createDto.Boxes.Select(Map).ToArray());

                var result = await sender.Send(command, cancellationToken);

                var dto = Map(result.Value);

                return result.Match(() => Results.Created($"/clocks/{dto.Id}", dto), CustomResults.Problem);
            })
            .WithTags(Tags.Clocks).WithOpenApi();
    }

    private static BoxInput Map(BoxCreateDto boxCreateDto)
    {
        return new BoxInput(boxCreateDto.Position, boxCreateDto.Value);
    }

    private static BoxDto Map(Box box)
    {
        return new BoxDto(box.Id, box.Position, box.Value);
    }

    private static ClockDto Map(Clock clock)
    {
        return new ClockDto(clock.Id, clock.Size, clock.Boxes.Select(Map).ToArray(), clock.Difficulty);
    }
}