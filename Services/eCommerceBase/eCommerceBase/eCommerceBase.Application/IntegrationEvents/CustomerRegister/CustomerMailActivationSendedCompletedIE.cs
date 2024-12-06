using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;

namespace eCommerceBase.Application.IntegrationEvents.CustomerRegister
{
    [UrnType<CustomerMailActivationSendedCompletedIE>]
    public class CustomerMailActivationSendedCompletedIE(Guid eventId) : IOutboxMessage
    {
        public Guid EventId { get; set; } = eventId;
        public OutboxState State { get; set; } = OutboxState.Completed;
    }
}
