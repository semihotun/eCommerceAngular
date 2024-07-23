using Carter;
using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class SpecificationAttributeOptionEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/specificationAttributeOption");

            group.MapPost("/createspecificationAttributeOption", CreateSpecificationAttributeOption)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/updatespecificationAttributeOption", UpdateSpecificationAttributeOption)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/deletespecificationAttributeOption", DeleteSpecificationAttributeOption)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getspecificationAttributeOptionbyid", GetSpecificationAttributeOptionById)
                .Produces(StatusCodes.Status200OK, typeof(Result<SpecificationAttributeOption>))
                .AllowAnonymous();
            group.MapGet("/getallspecificationAttributeOption", GetAllSpecificationAttributeOption)
                .Produces(StatusCodes.Status200OK, typeof(Result<SpecificationAttributeOption>))
                .AllowAnonymous();
        }

        public static async Task<IResult> CreateSpecificationAttributeOption([FromBody] CreateSpecificationAttributeOptionCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> UpdateSpecificationAttributeOption([FromBody] UpdateSpecificationAttributeOptionCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> DeleteSpecificationAttributeOption([FromBody] DeleteSpecificationAttributeOptionCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> GetSpecificationAttributeOptionById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetSpecificationAttributeOptionByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }

        public static async Task<IResult> GetAllSpecificationAttributeOption(ISender sender)
        {
            var result = await sender.Send(new GetAllSpecificationAttributeOption());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }

}
