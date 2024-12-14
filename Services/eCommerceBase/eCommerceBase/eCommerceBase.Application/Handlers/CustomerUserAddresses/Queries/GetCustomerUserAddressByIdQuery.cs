using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries;
public record GetCustomerUserAddressByIdQuery(Guid Id) : IRequest<Result<CustomerUserAddress>>;
public class GetCustomerUserAdressByIdQueryHandler(IReadDbRepository<CustomerUserAddress> customerUserAdressRepository,
        ICacheService cacheService) : IRequestHandler<GetCustomerUserAddressByIdQuery,
        Result<CustomerUserAddress>>
{
    private readonly IReadDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<CustomerUserAddress>> Handle(GetCustomerUserAddressByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
        async () =>
        {
            var query = await _customerUserAdressRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
        cancellationToken);
    }
}