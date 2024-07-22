using eCommerceBase.Application.IntegrationEvents.AdminRoles;
using eCommerceBase.Insfrastructure.Utilities.AdminRole;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;
using MassTransit;

namespace eCommerceBase.Application.Extension
{
    /// <summary>
    ///  Add Consumer Or Producer
    /// </summary>
    public static class AddConsumerOrProducer
    {
        public static void AddPublishers(this IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.AddDirectProducer<AddUserRoleIntegrationEvent>();
        }
        public static void AddConsumers(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext ctx)
        {
            cfg.AddDirectConsumer<AddUserRoleIntegrationEvent>((x) => x.ConfigureConsumer<AddUserRoleIntegrationEventHandler>(ctx));
        }
    }
}
