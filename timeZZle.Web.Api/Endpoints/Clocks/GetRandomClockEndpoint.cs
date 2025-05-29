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

public class GetRandomClockEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Tags.Clocks}/random", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetRandomClockQuery();

            var result = await sender.Send(query, cancellationToken);

            var dto = result.Value.Adapt<ClockDto>();

            return result.Match(() => Results.Ok(dto), CustomResults.Problem);
        });
    }
}