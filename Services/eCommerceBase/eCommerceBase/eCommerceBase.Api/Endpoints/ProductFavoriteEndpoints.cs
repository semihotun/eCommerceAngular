using Carter;
using eCommerceBase.Application.Handlers.ProductFavorites.Commands;
using eCommerceBase.Application.Handlers.ProductFavorites.Queries;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductFavoriteEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productFavorite");

            group.MapPost("/create", CreateProductFavorite)
                .Produces(StatusCodes.Status200OK, typeof(Result<Guid>));
            group.MapPost("/delete", DeleteProductFavorite)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/getallfavoriteproduct", GetAllProductFavoriteByCustomerId)
                .Produces(StatusCodes.Status200OK, typeof(Result<PagedList<ProductFavoriteDTO>>));
        }
        public static async Task<IResult> CreateProductFavorite([FromBody] CreateProductFavoriteCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductFavorite([FromBody] DeleteProductFavoriteCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProductFavoriteByCustomerId([FromBody] GetAllProductFavoriteByCustomerIdQuery data, ISender sender)
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
