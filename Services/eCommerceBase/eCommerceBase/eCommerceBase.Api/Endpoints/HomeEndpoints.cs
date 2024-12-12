using Carter;
using eCommerceBase.Application.Handlers.Products.Queries;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.Result;
using MediatR;

namespace eCommerceBase.Api.Endpoints
{
    public class HomeEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/home");

            group.MapGet("/gethomedto", GetAllHome)
               .Produces(StatusCodes.Status200OK, typeof(Result<HomeDto>))
               .AllowAnonymous();
        }
        public static async Task<IResult> GetAllHome(ISender sender)
        {
            var result = await sender.Send(new GetHomeDTOQuery());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
