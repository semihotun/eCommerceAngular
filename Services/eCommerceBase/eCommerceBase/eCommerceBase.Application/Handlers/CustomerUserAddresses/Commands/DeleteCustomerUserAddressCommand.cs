using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Commands;
public record DeleteCustomerUserAddressCommand(Guid Id) : IRequest<Result>;
public class DeleteCustomerUserAdressCommandHandler(IWriteDbRepository<CustomerUserAddress> customerUserAdressRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteCustomerUserAddressCommand,
        Result>
{
    private readonly IWriteDbRepository<CustomerUserAddress> _customerUserAdressRepository = customerUserAdressRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteCustomerUserAddressCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _customerUserAdressRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _customerUserAdressRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:CustomerUserAddres");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}