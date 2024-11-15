using Carter;
using eCommerceBase.Application.Handlers.UserGroups.Commands;
using eCommerceBase.Application.Handlers.UserGroups.Queries;
using eCommerceBase.Application.Handlers.UserGroups.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class UserGroupEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/userGroup");

            group.MapPost("/create", CreateUserGroup)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateUserGroup)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteUserGroup)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetUserGroupById)
               .Produces(StatusCodes.Status200OK, typeof(Result<UserGroup>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllUserGroup)
                .Produces(StatusCodes.Status200OK, typeof(Result<UserGroup>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetUserGroupGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<UserGroupGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateUserGroup([FromBody] CreateUserGroupCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateUserGroup([FromBody] UpdateUserGroupCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteUserGroup([FromBody] DeleteUserGroupCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetUserGroupById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetUserGroupByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllUserGroup(ISender sender)
        {
            var result = await sender.Send(new GetAllUserGroup());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetUserGroupGrid([FromBody] GetUserGroupGridDTOQuery data, ISender sender)
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
