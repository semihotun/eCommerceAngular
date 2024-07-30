using Carter;
using eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
using eCommerceBase.Application.Handlers.SpecificationAttributes.Queries;
using eCommerceBase.Application.Handlers.SpecificationAttributes.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class SpecificationAttributeEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/specificationAttribute");

            group.MapPost("/create", CreateSpecificationAttribute)
                .Produces(StatusCodes.Status200OK, typeof(Result<SpecificationAttribute>))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/update", UpdateSpecificationAttribute)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteSpecificationAttribute)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetSpecificationAttributeById)
                .Produces(StatusCodes.Status200OK, typeof(Result<SpecificationAttribute>))
                .AllowAnonymous();
            group.MapGet("/getall", GetAllSpecificationAttribute)
                .Produces(StatusCodes.Status200OK, typeof(Result<SpecificationAttribute>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetSpecificationAttributeGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<SpecificationAttributeGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateSpecificationAttribute([FromBody] CreateSpecificationAttributeCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> UpdateSpecificationAttribute([FromBody] UpdateSpecificationAttributeCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> DeleteSpecificationAttribute([FromBody] DeleteSpecificationAttributeCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> GetSpecificationAttributeById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetSpecificationAttributeByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> GetAllSpecificationAttribute(ISender sender)
        {
            var result = await sender.Send(new GetAllSpecificationAttribute());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetSpecificationAttributeGrid([FromBody] GetSpecificationAttributeGridDTOQuery data, ISender sender)
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
