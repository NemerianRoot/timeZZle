using Microsoft.AspNetCore.Routing;

namespace timeZZle.Web.Api.Interfaces;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
