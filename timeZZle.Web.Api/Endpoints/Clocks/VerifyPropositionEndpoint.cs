using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using timeZZle.Application.Handlers.Puzzles;
using timeZZle.Dtos.Puzzles;
using timeZZle.Web.Api.Interfaces;
using timeZZle.Web.Api.Utils;

namespace timeZZle.Web.Api.Endpoints.Clocks;

public class VerifyPropositionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"api/{Tags.Clocks}/verify",
            async (PropositionDto dto, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = dto.Adapt<VerifyPropositionCommand>();

                var result = await sender.Send(command, cancellationToken);

                return result.Match(() => Results.Ok(result.Value), CustomResults.Problem);
            }).WithTags(Tags.Clocks).WithOpenApi();
    }
}