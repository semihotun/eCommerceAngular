using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;

namespace eCommerceBase.Application.Handlers.CustomerUsers.Commands;
public record UpdateCustomerUserCommand(string FirstName,
		string LastName) : IRequest<Result>;
public class UpdateCustomerUserCommandHandler(IWriteDbRepository<CustomerUser> customerUserRepository,
        UserScoped userScoped,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateCustomerUserCommand,
		Result>
{
    private readonly IWriteDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
    private readonly UserScoped _userScoped = userScoped;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateCustomerUserCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _customerUserRepository.GetAsync(u => u.Id == _userScoped.Id);
            if (data is not null)
            {
                data.UpdateCustomerUser(request.FirstName, request.LastName);
                _customerUserRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:CustomerUser");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}