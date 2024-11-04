using Carter;
using eCommerceBase.Application.Handlers.ProductSpecifications.Commands;
using eCommerceBase.Application.Handlers.ProductSpecifications.Queries.Dtos;
using eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductSpecificationEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productSpecification");

            group.MapPost("/create", CreateProductSpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/update", UpdateProductSpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProductSpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetProductSpecificationById)
               .Produces(StatusCodes.Status200OK, typeof(Result<ProductSpecification>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllProductSpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result<ProductSpecification>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetProductSpecificationGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ProductSpecificationGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateProductSpecification([FromBody] CreateProductSpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateProductSpecification([FromBody] UpdateProductSpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductSpecification([FromBody] DeleteProductSpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductSpecificationById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetProductSpecificationByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProductSpecification(ISender sender)
        {
            var result = await sender.Send(new GetAllProductSpecification());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductSpecificationGrid([FromBody] GetProductSpecificationGridDTOQuery data, ISender sender)
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
