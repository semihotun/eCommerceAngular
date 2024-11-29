using Carter;
using eCommerceBase.Application.Handlers.Products.Commands;
using eCommerceBase.Application.Handlers.Products.Queries;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/product");

            group.MapPost("/create", CreateProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/update", UpdateProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetProductById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Product>))
               .AllowAnonymous();
            group.MapGet("/getproductdetailbyid", GetProductDetailById)
             .Produces(StatusCodes.Status200OK, typeof(Result<Product>))
             .AllowAnonymous();
            group.MapGet("/getproductdetailbyslug", GetProductDetailBySlug)
            .Produces(StatusCodes.Status200OK, typeof(Result<Product>))
            .AllowAnonymous();
            group.MapGet("/getall", GetAllProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result<Product>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetProductGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ProductGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateProduct([FromBody] CreateProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateProduct([FromBody] UpdateProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProduct([FromBody] DeleteProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductDetailById([FromQuery] Guid? id, ISender sender)
        {
            var result = await sender.Send(new GetProductDTOByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductDetailBySlug([FromQuery] string? slug, ISender sender)
        {
            var result = await sender.Send(new GetProductDTOBySlugQuery(slug));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> GetProductById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProduct(ISender sender)
        {
            var result = await sender.Send(new GetAllProduct());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductGrid([FromBody] GetProductGridDTOQuery data, ISender sender)
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
