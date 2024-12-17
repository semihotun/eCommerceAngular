using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;
using eCommerceBase.Persistence.SearchEngine;
using MassTransit;

namespace eCommerceBase.Application.IntegrationEvents.Product
{
    [UrnType<UpdateProductSearchStartedIE>]
    public class UpdateProductSearchStartedIE(Guid id, string productName) : IOutboxMessage
    {
        public Guid Id { get; set; } = id;
        public string ProductName { get; set; } = productName;
        public Guid EventId { get; set; } = Guid.NewGuid();
        public OutboxState State { get; set; } = OutboxState.Started;
    }
    public class UpdateProductSearchStartedIEHandler(ICoreSearchEngineContext searchEngineContext) : IConsumer<UpdateProductSearchStartedIE>
    {
        private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;

        public async Task Consume(ConsumeContext<UpdateProductSearchStartedIE> context)
        {
            var message = context.Message;
            var indexName = _searchEngineContext.IndexName<ProductSearch>();
            var response = await _searchEngineContext.Client.UpdateAsync<ProductSearch, object>(
                id: message.Id,
                selector: u => u.Index(indexName)
                               .Doc(new
                               {
                                   ProductName = message.ProductName
                               })
                               .DocAsUpsert(false)
            );
            if (!response.IsValid)
            {
                throw new Exception($"Elasticsearch ERROR: {response.OriginalException.Message}", response.OriginalException);
            }
        }
    }
}
