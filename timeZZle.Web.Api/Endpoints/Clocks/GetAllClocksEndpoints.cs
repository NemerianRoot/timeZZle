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

public class GetAllClocksEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Tags.Clocks, async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetAllClocksQuery();

                var result = await sender.Send(query, cancellationToken);

                var dtos = result.Value.Select(Map).ToArray();

                return result.Match(() => Results.Ok(dtos), CustomResults.Problem);
            })
            .WithTags(Tags.Clocks).WithOpenApi();
    }
    
    private static ClockDto Map(Clock clock)

    {
        return new ClockDto(clock.Id, clock.Boxes.Count, clock.Boxes.Select(Map).ToArray(), clock.Difficulty);
    }

    private static BoxDto Map(Box box)
    {
        return new BoxDto(box.Id, box.Position, box.Value);
    }
}