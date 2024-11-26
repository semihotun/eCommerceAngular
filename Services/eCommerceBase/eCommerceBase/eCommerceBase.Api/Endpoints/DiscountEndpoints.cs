using Carter;
using eCommerceBase.Application.Handlers.Discounts.Commands;
using eCommerceBase.Application.Handlers.Discounts.Queries;
using eCommerceBase.Application.Handlers.Discounts.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class DiscountEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/discount");

            group.MapPost("/create", CreateDiscount)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/delete", DeleteDiscount)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetDiscountById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Discount>))
               .AllowAnonymous();
            group.MapPost("/getgrid", GetDiscountGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<DiscountGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateDiscount([FromBody] CreateDiscountCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteDiscount([FromBody] DeleteDiscountCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetDiscountById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetDiscountByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetDiscountGrid([FromBody] GetDiscountGridDTOQuery data, ISender sender)
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
