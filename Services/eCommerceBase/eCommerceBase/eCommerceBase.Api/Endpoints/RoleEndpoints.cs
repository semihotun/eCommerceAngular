using Carter;
using eCommerceBase.Application.Handlers.Roles.Queries;
using eCommerceBase.Application.Handlers.Roles.Queries.Dtos;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class RoleEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/role");

           
            group.MapPost("/getusergroupnotexistrolegrid", GetUserGroupNotExistRoleGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<GetUserGroupNotExistRoleGridDTO>>))
                .AllowAnonymous();
        }      
        public static async Task<IResult> GetUserGroupNotExistRoleGrid([FromBody] GetUserGroupNotExistRoleGridDTOQuery data, ISender sender)
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
