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

public class GetRandomClockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Tags.Clocks}/random", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetRandomClockQuery();

            var result = await sender.Send(query, cancellationToken);

            var dto = Map(result.Value);

            return result.Match(() => Results.Ok(dto), CustomResults.Problem);
        });
    }

    private static ClockDto Map(Clock clock)
    {
        return new ClockDto(clock.Id, clock.Size, clock.Boxes.Select(Map).ToArray(), clock.Difficulty);
    }

    private static BoxDto Map(Box box)
    {
        return new BoxDto(box.Id, box.Position, box.Value);
    }
}