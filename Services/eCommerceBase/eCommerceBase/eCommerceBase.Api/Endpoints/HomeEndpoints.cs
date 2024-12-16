using Carter;
using eCommerceBase.Application.Handlers.Products.Queries;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            group.MapPost("/gethomeshowcasedetailquery", GetHomeShowcaseDetail)
               .Produces(StatusCodes.Status200OK, typeof(Result<PagedList<GetHomeShowcaseDetailDTO>>))
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
        public static async Task<IResult> GetHomeShowcaseDetail([FromBody] GetHomeShowcaseDetailQuery data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
