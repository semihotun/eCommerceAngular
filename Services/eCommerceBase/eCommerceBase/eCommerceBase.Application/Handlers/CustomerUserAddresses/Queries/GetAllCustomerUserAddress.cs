using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries;
public record GetAllCustomerUserAddress() : IRequest<Result<List<CustomerUserAddress>>>;
public class GetAllCustomerUserAdressHandler(IReadDbRepository<CustomerUserAddress> customerUserAdressRepository,
        ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<GetAllCustomerUserAddress,
        Result<List<CustomerUserAddress>>>
{
    private readonly IReadDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;
    public async Task<Result<List<CustomerUserAddress>>> Handle(GetAllCustomerUserAddress request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, _userScoped,
        async () =>
        {
            var data = await _customerUserAdressRepository.Query().Where(x => x.CustomerUserId == _userScoped.Id).ToListAsync();
            return Result.SuccessDataResult(data!);
        },
        cancellationToken);
    }
}