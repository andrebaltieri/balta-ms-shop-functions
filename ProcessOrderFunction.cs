using System.Threading.Tasks;
using BaltaFunctions.Models;
using BaltaFunctions.Services;
using Microsoft.Azure.WebJobs;

namespace BaltaFunctions
{
    public class ProcessOrderFunction
    {
        private readonly NotificationService _notificationService;

        public ProcessOrderFunction(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [FunctionName("ProcessOrderFunction")]
        public async Task Run([ServiceBusTrigger("orders", Connection = "microsservicos_SERVICEBUS")] Order order)
        {

            await _notificationService.NotifyAsync("andre@balta.io", "message");
        }
    }
}
