using Carter;
using eCommerceBase.Application.Handlers.ProductShipmentInfoes.Commands;
using eCommerceBase.Application.Handlers.ProductShipmentInfoes.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductShipmentInfoEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productShipmentInfo");

            group.MapPost("/create", CreateProductShipmentInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateProductShipmentInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProductShipmentInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyproductid", GetProductShipmentInfoByProductId)
               .Produces(StatusCodes.Status200OK, typeof(Result<ProductShipmentInfo>))
               .AllowAnonymous();
        }
        public static async Task<IResult> CreateProductShipmentInfo([FromBody] CreateProductShipmentInfoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateProductShipmentInfo([FromBody] UpdateProductShipmentInfoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductShipmentInfo([FromBody] DeleteProductShipmentInfoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductShipmentInfoByProductId([FromQuery] Guid productId, ISender sender)
        {
            var result = await sender.Send(new GetProductShipmentInfoByProductIdQuery(productId));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
