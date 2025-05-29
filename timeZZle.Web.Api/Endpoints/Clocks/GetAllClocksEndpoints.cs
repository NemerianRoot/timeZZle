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

public class GetAllClocksEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Tags.Clocks, async (ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetAllClocksQuery();

                var result = await sender.Send(query, cancellationToken);

                var dtos = result.Value.Adapt<ClockDto>();

                return result.Match(() => Results.Ok(dtos), CustomResults.Problem);
            })
            .WithTags(Tags.Clocks).WithOpenApi();
    }
}