using Carter;
using eCommerceBase.Application.Handlers.CustomerUsers.Commands;
using eCommerceBase.Application.Handlers.CustomerUsers.Queries;
using eCommerceBase.Application.Handlers.CustomerUsers.Queries.Dtos;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CustomerUserEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/customeruser");

            group.MapPost("/customeruserlogin", CustomerUserLogin)
                .Produces(StatusCodes.Status200OK, typeof(Result<AccessToken>))
                .AllowAnonymous();
            group.MapPost("/customeruserregister", CustomerUserRegister)
                .Produces(StatusCodes.Status200OK, typeof(Result<AccessToken>))
                .AllowAnonymous();
            group.MapPost("/customeruseractivationconfirmation", CustomerUserActivationConfirmation)
                 .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/updatecustomeruser", UpdateCustomerUser)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/getcustomeruserbyid", GetCustomerUserById)
                  .Produces(StatusCodes.Status200OK, typeof(Result<CustomerUserDTO>));
            group.MapPost("/verifymailsend", VerifyMailSend)
                .Produces(StatusCodes.Status200OK, typeof(Result<CustomerUserDTO>));
        }
        public static async Task<IResult> CustomerUserLogin([FromBody] CustomerUserLoginCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> CustomerUserRegister([FromBody] CustomerUserRegisterCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> CustomerUserActivationConfirmation([FromBody] CustomerUserActivationConfirmationCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCustomerUserById(ISender sender)
        {
            var result = await sender.Send(new GetCustomerUserByIdDTOQuery());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateCustomerUser([FromBody] UpdateCustomerUserCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> VerifyMailSend(ISender sender)
        {
            var result = await sender.Send(new VerifyMailSendCommand());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
