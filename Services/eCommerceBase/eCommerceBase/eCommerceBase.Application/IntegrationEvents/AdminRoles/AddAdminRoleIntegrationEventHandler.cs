using eCommerceBase.Application.Handlers.Roles.Commands;
using eCommerceBase.Insfrastructure.Utilities.AdminRole;
using MassTransit;
using MediatR;
namespace eCommerceBase.Application.IntegrationEvents.AdminRoles
{
    public class AddUserRoleIntegrationEventHandler(IMediator mediator) : IConsumer<AddUserRoleIntegrationEvent>
    {
        private readonly IMediator _mediator = mediator;
        public async Task Consume(ConsumeContext<AddUserRoleIntegrationEvent> context)
        {
            await _mediator.Send(new RoleCommand(context.Message.RoleName));
        }
    }
}
