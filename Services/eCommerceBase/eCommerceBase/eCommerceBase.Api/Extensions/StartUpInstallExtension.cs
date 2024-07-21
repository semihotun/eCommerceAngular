using Carter;
using eCommerceBase.Application.Assemblies;
using eCommerceBase.Application.Extension;
using eCommerceBase.Insfrastructure.Utilities.ApiDoc.Swagger;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Cors;
using eCommerceBase.Insfrastructure.Utilities.HangFire;
using eCommerceBase.Insfrastructure.Utilities.Identity;
using eCommerceBase.Insfrastructure.Utilities.Logging;
using eCommerceBase.Insfrastructure.Utilities.MediatR;
using eCommerceBase.Insfrastructure.Utilities.ServiceBus;
using eCommerceBase.Insfrastructure.Utilities.Telemetry;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Persistence.Extensions;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.SearchEngine;
using eCommerceBase.Persistence.UnitOfWork;
using FluentValidation;

namespace eCommerceBase.Extensions
{
    public static class StartUpInstallExtension
    {
        public static async Task AddStartupServicesAsync(this WebApplicationBuilder builder)
        {
            builder.Services.AddCarter();
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();
            builder.AddCustomSwaggerGen();
            builder.AddCors();
            builder.Services.AddMvc();
            //Log-Cache-Mediatr-FluentValidation-Mass transit
            builder.AddSerilog();
            //builder.AddTelemeter();
            builder.AddRedis();
            var assembly = ApiAssemblyExtensions.GetLibrariesAssemblies();
            //builder.AddHangFire(assembly);
            await builder.Services.ConfigureDbContextAsync(builder.Configuration);
            builder.AddMediatR(assembly);
            builder.Services.AddValidatorsFromAssembly(ApplicationAssemblyExtension.GetApplicationAssembly(), includeInternalTypes: true);
            builder.AddCustomMassTransit(assembly, (busRegistrationContext, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.AddConsumers(busRegistrationContext);
                busFactoryConfigurator.AddPublishers();
            });
            builder.AddIdentitySettings();
            //Service Registered
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICoreDbContext, CoreDbContext>();
            builder.Services.AddScoped(typeof(IWriteDbRepository<>), typeof(WriteDbRepository<>));
            builder.Services.AddScoped(typeof(IReadDbRepository<>), typeof(ReadDbRepository<>));
            await SearchEngineRegistration.MigrateElasticDbAsync(assembly, builder.Configuration);
            builder.Services.AddScoped<ICoreSearchEngineContext, CoreSearchEngineContext>();
        }
    }
}
