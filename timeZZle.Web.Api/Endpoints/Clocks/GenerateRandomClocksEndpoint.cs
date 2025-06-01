using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using timeZZle.Application.Handlers.Clocks;
using timeZZle.Dtos.Puzzles;
using timeZZle.Web.Api.Interfaces;
using timeZZle.Web.Api.Utils;

namespace timeZZle.Web.Api.Endpoints.Clocks;

public class GenerateRandomClocksEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"api/{Tags.Clocks}/seed",
            async (ClockRandomGenerateDto dto, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GenerateRandomClocksCommand(dto.ClockSize, dto.BatchSize);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            }).WithTags(Tags.Clocks).WithOpenApi();
    }
}