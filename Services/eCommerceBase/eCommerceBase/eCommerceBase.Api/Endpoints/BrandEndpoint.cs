﻿using Carter;
using eCommerceBase.Application.Handlers.Brands.Commands;
using eCommerceBase.Application.Handlers.Brands.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class BrandEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/brand");

            group.MapPost("/createbrand", CreateBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/updatebrand", UpdateBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/deletebrand", DeleteBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbrandbyid", GetBrandById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Brand>))
               .AllowAnonymous();
            group.MapGet("/getallbrand", GetAllBrand)
            .Produces(StatusCodes.Status200OK, typeof(Result<Brand>))
            .AllowAnonymous();
        }
        public static async Task<IResult> CreateBrand([FromBody] CreateBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateBrand([FromBody] UpdateBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteBrand([FromBody] DeleteBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetBrandById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetBrandByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllBrand(ISender sender)
        {
            var result = await sender.Send(new GetAllBrand());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}