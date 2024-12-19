using Carter;
using eCommerceBase.Application.Handlers.CategorySpecifications.Commands;
using eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
using eCommerceBase.Application.Handlers.CategorySpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CategorySpecificationEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/categorySpecification");

            group.MapPost("/create", CreateCategorySpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateCategorySpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteCategorySpecification)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetCategorySpecificationById)
               .Produces(StatusCodes.Status200OK, typeof(Result<CategorySpecification>))
               .AllowAnonymous();
            group.MapPost("/getallnotexistspecificationgrid", GetAllNotExistSpecificationGridDTOQuery)
                .Produces(StatusCodes.Status200OK, typeof(Result<PagedList<AllNotExistSpecificationGridDTO>>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetCategorySpecificationGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<AllSpecificationGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateCategorySpecification([FromBody] CreateCategorySpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateCategorySpecification([FromBody] UpdateCategorySpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteCategorySpecification([FromBody] DeleteCategorySpecificationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCategorySpecificationById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetCategorySpecificationByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllNotExistSpecificationGridDTOQuery([FromBody] GetAllNotExistSpecificationGridDTOQuery data,ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCategorySpecificationGrid([FromBody] GetAllSpecificationGridDTOQuery data, ISender sender)
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
