using Carter;
using eCommerceBase.Application.Handlers.ProductFavorites.Commands;
using eCommerceBase.Domain.Result;
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
    }
}
