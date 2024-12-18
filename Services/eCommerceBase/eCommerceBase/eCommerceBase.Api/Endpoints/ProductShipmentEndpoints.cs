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
                .Produces(StatusCodes.Status200OK, typeof(Result<ProductShipmentInfo>));
            group.MapPost("/update", UpdateProductShipmentInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result<ProductShipmentInfo>))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProductShipmentInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/getproductshipmentinfobyproductid", GetProductShipmentInfoByProductId)
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
        public static async Task<IResult> GetProductShipmentInfoByProductId([FromBody] GetProductShipmentInfoByProductIdQuery data, ISender sender)
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
