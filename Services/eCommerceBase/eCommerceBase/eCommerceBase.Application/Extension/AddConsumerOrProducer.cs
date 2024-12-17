using eCommerceBase.Application.IntegrationEvents.AdminRoles;
using eCommerceBase.Application.IntegrationEvents.CustomerRegister;
using eCommerceBase.Application.IntegrationEvents.Product;
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
            ///Customer User
            cfg.AddDirectProducer<CustomerMailActivationSendedStartedIE>();
            cfg.AddDirectProducer<CustomerMailActivationSendedCompletedIE>();
            cfg.AddDirectProducer<CustomerMailActivationSendedFailedIE>();
            ///Product Search
            cfg.AddDirectProducer<CreateProductSearchStartedIE>();
            cfg.AddDirectProducer<UpdateProductSearchStartedIE>();
        }
        public static void AddConsumers(this IRabbitMqBusFactoryConfigurator cfg, IBusRegistrationContext ctx)
        {
            cfg.AddDirectConsumer<AddUserRoleIntegrationEvent>((x) => x.ConfigureConsumer<AddUserRoleIntegrationEventHandler>(ctx));
            ///Customer User
            cfg.AddDirectConsumer<CustomerMailActivationSendedStartedIE>((x) =>
            {
                x.ConfigureConsumer<CustomerMailActivationSendedStartedIEHandler>(ctx);
            });
            //Product Search
            cfg.AddDirectConsumer<CreateProductSearchStartedIE>((x) =>
            {
                x.ConfigureConsumer<CreateProductSearchStartedIEHandler>(ctx);
            });
            cfg.AddDirectConsumer<UpdateProductSearchStartedIE>((x) =>
            {
                x.ConfigureConsumer<UpdateProductSearchStartedIEHandler>(ctx);
            });
        }
    }
}
