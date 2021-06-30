using BaltaFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BaltaFunctions.Startup))]
namespace BaltaFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<MessageBusService>();
            builder.Services.AddTransient<NotificationService>();
        }
    }
}
