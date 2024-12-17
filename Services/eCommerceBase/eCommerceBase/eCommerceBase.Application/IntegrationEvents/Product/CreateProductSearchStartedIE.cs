using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;
using eCommerceBase.Persistence.SearchEngine;
using MassTransit;

namespace eCommerceBase.Application.IntegrationEvents.Product
{
    [UrnType<CreateProductSearchStartedIE>]
    public class CreateProductSearchStartedIE(Guid id, string productName) : IOutboxMessage
    {
        public Guid Id { get; set; } = id;
        public string ProductName { get; set; } = productName;
        public Guid EventId { get; set; } = Guid.NewGuid();
        public OutboxState State { get; set; } = OutboxState.Started;
    }
    public class CreateProductSearchStartedIEHandler(ICoreSearchEngineContext searchEngineContext) : IConsumer<CreateProductSearchStartedIE>
    {
        private readonly ICoreSearchEngineContext _searchEngineContext = searchEngineContext;

        public async Task Consume(ConsumeContext<CreateProductSearchStartedIE> context)
        {
            var message = context.Message;
            var productSearch = new ProductSearch
            {
                Id = message.Id,
                ProductName = message.ProductName
            };
            var indexName = _searchEngineContext.IndexName<ProductSearch>();
            var response = await _searchEngineContext.Client.IndexAsync(productSearch, idx => idx.Index(indexName));
            if (!response.IsValid)
            {
                throw new Exception($"Elasticsearch ERROR: {response.OriginalException.Message}", response.OriginalException);
            }
        }
    }
}
