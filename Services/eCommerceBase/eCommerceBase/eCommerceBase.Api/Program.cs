using Carter;
using eCommerceBase.Application.Assemblies;
using eCommerceBase.Application.Jobs;
using eCommerceBase.Extensions;
using eCommerceBase.Insfrastructure.Utilities.Exceptions.GlobalEror;
using eCommerceBase.Insfrastructure.Utilities.HangFire;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Insfrastructure.Utilities.Ioc;
using eCommerceBase.Insfrastructure.Utilities.Outboxes;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);
await builder.AddStartupServicesAsync();
var app = builder.Build();
GlobalConfiguration.Configuration.UseActivator(new HangfireJobActivator(app.Services));

if (!app.Environment.IsDevelopment())
{
    //Metrics Geçici Kapalý
    //app.UseHttpMetrics();
    //app.MapMetrics();
    app.UseHttpsRedirection();
}
//WebUI
app.UseSwagger();
app.UseSwaggerUI();
//Core
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//MiddleWare
ServiceTool.ServiceProvider = app.Services;
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<ClaimMiddleware>();
app.UseAntiforgery();
app.MapCarter();
var task = app.RunAsync();
_ = app.GenerateDbRole();
HangFireJobs.AddAllStartupJobs();
await app.AddOutboxKafkaConsumerAsync(ApplicationAssemblyExtension.GetApplicationAssembly());
await task;