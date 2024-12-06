using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;

namespace eCommerceBase.Application.IntegrationEvents.CustomerRegister
{
    [UrnType<CustomerMailActivationSendedFailedIE>]
    public class CustomerMailActivationSendedFailedIE(Guid eventId) : IOutboxMessage
    {
        public Guid EventId { get; set; } = eventId;
        public OutboxState State { get; set; } = OutboxState.CanceledCompleted;
    }
}
