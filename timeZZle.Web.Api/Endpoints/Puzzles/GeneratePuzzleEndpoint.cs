using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using timeZZle.Application.Handlers.Puzzles;
using timeZZle.Dtos.Puzzles;
using timeZZle.Web.Api.Interfaces;
using timeZZle.Web.Api.Utils;

namespace timeZZle.Web.Api.Endpoints.Puzzles;

public class GeneratePuzzleEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Tags.Puzzles,
            async (PuzzleGenerateDto dto, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new GeneratePuzzleCommand(dto.ClockSize, dto.BatchSize);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, CustomResults.Problem);
            });
    }
}