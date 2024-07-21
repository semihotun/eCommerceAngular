using eCommerceBase.Application.Handlers.User.Commands;
using eCommerceBase.Insfrastructure.Utilities.AdminRole;
using MassTransit;
using MediatR;
namespace eCommerceBase.Application.IntegrationEvents.AdminRoles
{
    public class AddAdminRoleIntegrationEventHandler(IMediator mediator) : IConsumer<AddAdminRoleIntegrationEvent>
    {
        private readonly IMediator _mediator = mediator;
        public async Task Consume(ConsumeContext<AddAdminRoleIntegrationEvent> context)
        {
            await _mediator.Send(new UserRoleCommand(context.Message.RoleName));
        }
    }
}
