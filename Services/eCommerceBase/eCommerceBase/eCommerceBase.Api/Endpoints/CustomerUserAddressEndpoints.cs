using Carter;
using eCommerceBase.Application.Handlers.CustomerUserAddresses.Commands;
using eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries;
using eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CustomerUserAddressEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/customerUserAddress");

            group.MapPost("/create", CreateCustomerUserAddress)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateCustomerUserAddress)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/delete", DeleteCustomerUserAddress)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapGet("/getbyid", GetCustomerUserAddressById)
               .Produces(StatusCodes.Status200OK, typeof(Result<CustomerUserAddress>));
            group.MapGet("/getall", GetAllCustomerUserAddress)
                .Produces(StatusCodes.Status200OK, typeof(Result<CustomerUserAddress>));
            group.MapGet("/getallcustomeruseradressdto", GetAllCustomerUserAddressDTO)
                .Produces(StatusCodes.Status200OK, typeof(Result<List<GetAllCustomerUserAddressDTO>>));
        }
        public static async Task<IResult> CreateCustomerUserAddress([FromBody] CreateCustomerUserAddressCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateCustomerUserAddress([FromBody] UpdateCustomerUserAddressCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteCustomerUserAddress([FromBody] DeleteCustomerUserAddressCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCustomerUserAddressById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetCustomerUserAddressByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllCustomerUserAddress(ISender sender)
        {
            var result = await sender.Send(new GetAllCustomerUserAddress());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllCustomerUserAddressDTO(ISender sender)
        {
            var result = await sender.Send(new GetAllCustomerUserAddressDTOQuery());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }     
    }
}
