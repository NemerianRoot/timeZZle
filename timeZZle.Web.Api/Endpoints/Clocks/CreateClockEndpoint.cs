using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using timeZZle.Application.Handlers.Clocks;
using timeZZle.Dtos.Clocks;
using timeZZle.Web.Api.Interfaces;
using timeZZle.Web.Api.Utils;

namespace timeZZle.Web.Api.Endpoints.Clocks;

public class CreateClockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Tags.Clocks, async (ClockCreateDto createDto, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateClockCommand(createDto.Difficulty, createDto.Boxes.Select(Map).ToArray());

                var result = await sender.Send(command, cancellationToken);

                var dto = result.Value.Adapt<ClockDto>();

                return result.Match(() => Results.Created($"/clocks/{dto.Id}", dto), CustomResults.Problem);
            }).WithTags(Tags.Clocks).WithOpenApi();
    }

    private static BoxInput Map(BoxCreateDto boxCreateDto)
    {
        return new BoxInput(boxCreateDto.Position, boxCreateDto.Value);
    }
}