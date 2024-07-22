using Carter;
using eCommerceBase.Application.Handlers.AdminUsers.Commands;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using MassTransit.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class AdminUserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/adminuser");

            group.MapPost("/adminuserlogin", AdminUserLogin)
                .Produces(StatusCodes.Status200OK, typeof(Result<AccessToken>))
                .AllowAnonymous();
            group.MapPost("/adminuserregister", AdminUserRegister)
             .Produces(StatusCodes.Status200OK, typeof(Result<AccessToken>))
             .AllowAnonymous();
        }
        public static async Task<IResult> AdminUserLogin([FromBody] AdminUserLoginCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> AdminUserRegister([FromBody] AdminUserRegisterCommand data, ISender sender)
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
