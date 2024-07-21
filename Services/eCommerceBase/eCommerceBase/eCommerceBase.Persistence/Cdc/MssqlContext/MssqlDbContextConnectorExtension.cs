using eCommerceBase.Insfrastructure.Utilities.Kafka;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using Microsoft.Extensions.Configuration;

namespace eCommerceBase.Persistence.Cdc.MssqlContext
{
    public static class MssqlDbContextConnectorExtension
    {
        public static async Task AddAllConnectorAsync(IConfiguration configuration)
        {
            await KafkaExtension.AddConnector(configuration,
                SkippedOperation.Delete,
                "Outbox",
                nameof(Outbox));
        }
    }
}
