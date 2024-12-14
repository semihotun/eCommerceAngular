using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Commands;
public record CreateCustomerUserAddressCommand(string Name,
        Guid CityId,
        Guid DistrictId,
        string Street,
        string Address) : IRequest<Result>;
public class CreateCustomerUserAdressCommandHandler(IWriteDbRepository<CustomerUserAddress> customerUserAdressRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService, UserScoped userScoped) : IRequestHandler<CreateCustomerUserAddressCommand,
        Result>
{
    private readonly IWriteDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;
    public async Task<Result> Handle(CreateCustomerUserAddressCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = new CustomerUserAddress(request.Name, request.CityId, request.DistrictId, request.Street, request.Address, _userScoped.Id);
            await _customerUserAdressRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:CustomerUserAddres");
            return Result.SuccessResult(Messages.Added);
        });
    }
}