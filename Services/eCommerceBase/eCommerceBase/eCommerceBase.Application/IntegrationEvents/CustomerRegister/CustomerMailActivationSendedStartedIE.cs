using eCommerceBase.Application.Handlers.MailInfos.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.SearchEngine;
using eCommerceBase.Persistence.UnitOfWork;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace eCommerceBase.Application.IntegrationEvents.CustomerRegister
{
    [UrnType<CustomerMailActivationSendedStartedIE>]
    public class CustomerMailActivationSendedStartedIE(Guid customerId, string email, string firstName,
        string lastName) : IOutboxMessage
    {
        public Guid CustomerId { get; set; } = customerId;
        public string Email { get; set; } = email;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Guid EventId { get; set; } = Guid.NewGuid();
        public OutboxState State { get; set; } = OutboxState.Started;
    }

    public class CustomerMailActivationSendedStartedIEHandler(ISender sender, IUnitOfWork unitOfWork, ICoreSearchEngineContext searchEngineContext,
        IReadDbRepository<MailTemplate> mailTemplateRepository,
        IWriteDbRepository<CustomerActivationCode> customerActivationCodeRepository,
        IConfiguration configuration
        ) : IConsumer<CustomerMailActivationSendedStartedIE>
    {
        private readonly ISender _sender = sender;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;
        private readonly IReadDbRepository<MailTemplate> _mailTemplateRepository = mailTemplateRepository;
        private readonly IWriteDbRepository<CustomerActivationCode> _customerActivationCodeRepository = customerActivationCodeRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task Consume(ConsumeContext<CustomerMailActivationSendedStartedIE> context)
        {
            await _unitOfWork.BeginTransactionAndCreateOutbox(async (setOutBoxMessage) =>
            {
                var request = context.Message;
                var mailTemplate = await _mailTemplateRepository.GetByIdAsync(Guid.Parse(InitConst.ActivationMailId));

                var activationCode = Guid.NewGuid();
                var customerActivationCode = (await _customerActivationCodeRepository.GetAsync(x => x.CustomerUserId == request.CustomerId));
                if (customerActivationCode != null)
                {
                    if (customerActivationCode.ValidtiyDate > DateTime.Now)
                    {
                        activationCode = customerActivationCode.ActivationCode;
                    }
                    else
                    {
                        var updateCustomerActivation = new CustomerActivationCode(customerActivationCode.CustomerUserId, activationCode, DateTime.Now.AddDays(1));
                        updateCustomerActivation.SetId(customerActivationCode.Id);
                        _customerActivationCodeRepository.Update(updateCustomerActivation);
                    }
                }
                else
                {
                    await _customerActivationCodeRepository.AddAsync(new CustomerActivationCode(request.CustomerId, activationCode, DateTime.Now.AddDays(1)));
                }
                var content = mailTemplate!.TemplateContent
                    .Replace("#{{email}}", request.Email)
                    .Replace("#{{firstName}}", request.FirstName)
                    .Replace("#{{lastName}}", request.LastName)
                    .Replace("#{{activationCode}}", _configuration["WebApp_BaseUrl"]+"/" + "activation-code/" + activationCode);

                var response = await _sender.Send(new MailSendCommand([request.Email], content, mailTemplate.TemplateHeader));
                if (response.Success)
                {
                    setOutBoxMessage(new CustomerMailActivationSendedCompletedIE(request.EventId));
                }
                else
                {
                    setOutBoxMessage(new CustomerMailActivationSendedFailedIE(request.EventId));
                }
            });
        }
    }
}
