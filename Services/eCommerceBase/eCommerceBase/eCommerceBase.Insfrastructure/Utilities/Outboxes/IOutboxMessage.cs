using eCommerceBase.Insfrastructure.Utilities.ServiceBus;

namespace eCommerceBase.Insfrastructure.Utilities.Outboxes
{
    public interface IOutboxMessage : IMessage
    {
        public Guid EventId { get; set; }
        public OutboxState State { get; set; }
    }
}
