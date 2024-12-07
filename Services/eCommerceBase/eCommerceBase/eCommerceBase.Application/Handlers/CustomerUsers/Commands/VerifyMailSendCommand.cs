using eCommerceBase.Application.Constants;
using eCommerceBase.Application.IntegrationEvents.CustomerRegister;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Insfrastructure.Utilities.Identity.Service;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;
namespace eCommerceBase.Application.Handlers.CustomerUsers.Commands
{
    public record VerifyMailSendCommand() : IRequest<Result>;

    public class VerifyMailSendCommandHandler(
        IWriteDbRepository<CustomerUser> customerUserRepository,
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        UserScoped userScoped) : IRequestHandler<VerifyMailSendCommand, Result>
    {
        private readonly IWriteDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;
        private readonly UserScoped _userScoped = userScoped;

        public async Task<Result> Handle(VerifyMailSendCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransactionAndCreateOutbox<Result>(async (successAction) =>
            {
                var customerUser = await _customerUserRepository.GetByIdAsync(_userScoped.Id);
                if (customerUser == null)
                {
                    return Result.ErrorResult(Messages.MailNotSended);
                }

                if (customerUser.IsActivationApprove)
                {
                    return Result.ErrorResult(Messages.AccountAlreadyConfirmed);
                }
                var outBoxMessage = new CustomerMailActivationSendedStartedIE(customerUser.Id, customerUser.Email,
                customerUser.FirstName, customerUser.LastName);
                successAction(outBoxMessage);
                return Result.SuccessResult(Messages.MailSended);
            });
        }
    }
}
