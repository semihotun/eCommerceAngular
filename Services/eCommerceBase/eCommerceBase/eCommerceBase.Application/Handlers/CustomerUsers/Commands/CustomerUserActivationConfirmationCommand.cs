using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.CustomerUsers.Commands
{
    public record CustomerUserActivationConfirmationCommand(Guid ActivationCode) : IRequest<Result>;

    public class CustomerUserActivationConfirmationCommandHandler(
        IWriteDbRepository<CustomerActivationCode> customerActivationCodeRepository,
        IWriteDbRepository<CustomerUser> customerUserRepository,
        IUnitOfWork unitOfWork,
        UserScoped userScoped
        ) : IRequestHandler<CustomerUserActivationConfirmationCommand, Result>
    {
        private readonly IWriteDbRepository<CustomerActivationCode> _customerActivationCodeRepository = customerActivationCodeRepository;
        private readonly IWriteDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly UserScoped _userScoped = userScoped;

        public async Task<Result> Handle(CustomerUserActivationConfirmationCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction(async () =>
            {
                var customerUser = await _customerUserRepository.GetByIdAsync(_userScoped.Id);
                if (customerUser!.IsActivationApprove)
                    return Result.ErrorResult(Messages.UserHasAldreadyBeenApproved);

                var isActivationCodeTrue = await _customerActivationCodeRepository.AnyAsync(x => x.CustomerUserId == _userScoped.Id
                   && x.ActivationCode == request.ActivationCode && x.ValidtiyDate > DateTime.Now);

                if (isActivationCodeTrue)
                {
                    customerUser!.SetIsActivationApprove(true);
                    return Result.SuccessResult(Messages.EmailConfirmed);
                }
                else
                {
                    return Result.ErrorResult(Messages.ActivationCodeWrongOrOutOfDate);
                }
            });


        }
    }
}

