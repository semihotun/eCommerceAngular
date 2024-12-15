using eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries;
public record GetAllCustomerUserAddressDTOQuery()
    : IRequest<Result<List<GetAllCustomerUserAddressDTO>>>;
public class GetAllCustomerUserAddressDTOQueryHandler(IReadDbRepository<CustomerUserAddress> customerUserAdressRepository,
    ICacheService cacheService, UserScoped userScoped)
    : IRequestHandler<GetAllCustomerUserAddressDTOQuery, Result<List<GetAllCustomerUserAddressDTO>>>
{
    private readonly IReadDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;
    public async Task<Result<List<GetAllCustomerUserAddressDTO>>> Handle(GetAllCustomerUserAddressDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, _userScoped, async () =>
        {
            var query = await _customerUserAdressRepository.Query()
            .Where(x => x.CustomerUserId == _userScoped.Id)
            .Select(x => new GetAllCustomerUserAddressDTO
            {
                Id = x.Id,
                Name = x.Name,
                DistrictName = x.District!.Name,
                CityName = x.City!.Name,
                Street = x.Street,
                Address = x.Address
            }).ToListAsync();
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}