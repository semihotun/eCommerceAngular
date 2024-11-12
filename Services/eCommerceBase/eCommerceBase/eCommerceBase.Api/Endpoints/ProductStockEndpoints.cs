using Carter;
using eCommerceBase.Application.Handlers.ProductStocks.Commands;
using eCommerceBase.Application.Handlers.ProductStocks.Queries;
using eCommerceBase.Application.Handlers.ProductStocks.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductStockEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productStock");

            group.MapPost("/create", CreateProductStock)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateProductStock)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProductStock)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetProductStockById)
               .Produces(StatusCodes.Status200OK, typeof(Result<ProductStock>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllProductStock)
                .Produces(StatusCodes.Status200OK, typeof(Result<ProductStock>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetProductStockGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ProductStockGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateProductStock([FromBody] CreateProductStockCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateProductStock([FromBody] UpdateProductStockCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductStock([FromBody] DeleteProductStockCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductStockById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetProductStockByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProductStock(ISender sender)
        {
            var result = await sender.Send(new GetAllProductStock());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductStockGrid([FromBody] GetProductStockGridDTOQuery data, ISender sender)
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
