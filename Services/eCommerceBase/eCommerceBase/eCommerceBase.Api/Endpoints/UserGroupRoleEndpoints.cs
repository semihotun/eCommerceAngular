using Carter;
using eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
using eCommerceBase.Application.Handlers.UserGroupRoles.Queries;
using eCommerceBase.Application.Handlers.UserGroupRoles.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class UserGroupRoleRoleEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/userGroupRole");

            group.MapPost("/create", CreateUserGroupRole)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateUserGroupRole)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteUserGroupRole)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetUserGroupRoleById)
               .Produces(StatusCodes.Status200OK, typeof(Result<UserGroupRole>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllUserGroupRole)
                .Produces(StatusCodes.Status200OK, typeof(Result<UserGroupRole>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetUserGroupRoleGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<UserGroupRoleGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateUserGroupRole([FromBody] CreateUserGroupRoleCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateUserGroupRole([FromBody] UpdateUserGroupRoleCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteUserGroupRole([FromBody] DeleteUserGroupRoleCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetUserGroupRoleById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetUserGroupRoleByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllUserGroupRole(ISender sender)
        {
            var result = await sender.Send(new GetAllUserGroupRole());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetUserGroupRoleGrid([FromBody] GetUserGroupRoleGridDTOQuery data, ISender sender)
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
