using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BaltaFunctions.ViewModels;
using Microsoft.Extensions.Configuration;

namespace BaltaFunctions.Services
{
    public class NotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration) => _configuration = configuration;

        public async Task NotifyAsync(string customer, string message)
        {
            var notification = new DiscordNotificationModel(
                _configuration["DiscordWebHookUrl"],
                message,
                customer);


            var httpClient = new HttpClient();
            var data = JsonSerializer.Serialize(notification, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(notification.WebHookUrl, content);
        }
    }
}
