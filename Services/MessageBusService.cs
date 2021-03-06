using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using BaltaFunctions.Models;
using Microsoft.Extensions.Configuration;

namespace BaltaFunctions.Services
{
    public class MessageBusService
    {
        private readonly IConfiguration _configuration;

        public MessageBusService(IConfiguration configuration) => _configuration = configuration;

        public async Task SendAsync(Order order)
        {
            await SendAsync(order, "orders");
        }

        public async Task SendAsync(Order order, string queue)
        {
            var connectionString = _configuration["ServiceBusConnectionString"];

            await using var client = new ServiceBusClient(connectionString);

            var sender = client.CreateSender(queue);
            var json = JsonSerializer.Serialize(order);
            var message = new ServiceBusMessage(json);

            await sender.SendMessageAsync(message);
        }
    }
}
