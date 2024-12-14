using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Commands;
public record UpdateCustomerUserAddressCommand(string Name,
        Guid CityId,
        Guid DistrictId,
        string Street,
        string Address,
        Guid Id) : IRequest<Result>;
public class UpdateCustomerUserAdressCommandHandler(IWriteDbRepository<CustomerUserAddress> customerUserAdressRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<UpdateCustomerUserAddressCommand,
        Result>
{
    private readonly IWriteDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;
    public async Task<Result> Handle(UpdateCustomerUserAddressCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _customerUserAdressRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                var newData = new CustomerUserAddress(request.Name, request.CityId, request.DistrictId, request.Street, request.Address, _userScoped.Id)
                {
                    Id = data.Id
                };
                _customerUserAdressRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:CustomerUserAddres");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}