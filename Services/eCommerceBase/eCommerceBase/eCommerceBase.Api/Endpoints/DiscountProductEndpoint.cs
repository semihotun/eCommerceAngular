using Carter;
using eCommerceBase.Application.Handlers.DiscountProducts.Commands;
using eCommerceBase.Application.Handlers.DiscountProducts.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class DiscountProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/discountProduct");

            group.MapPost("/create", CreateDiscountProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateDiscountProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteDiscountProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetDiscountProductById)
               .Produces(StatusCodes.Status200OK, typeof(Result<DiscountProduct>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllDiscountProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result<DiscountProduct>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateDiscountProduct([FromBody] CreateDiscountProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateDiscountProduct([FromBody] UpdateDiscountProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteDiscountProduct([FromBody] DeleteDiscountProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetDiscountProductById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetDiscountProductByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllDiscountProduct(ISender sender)
        {
            var result = await sender.Send(new GetAllDiscountProduct());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
