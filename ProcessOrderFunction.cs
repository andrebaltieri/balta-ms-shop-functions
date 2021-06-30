using System.Threading;
using System.Threading.Tasks;
using BaltaFunctions.Clients;
using BaltaFunctions.Enums;
using BaltaFunctions.Models;
using BaltaFunctions.Services;
using BaltaFunctions.ViewModels;
using Microsoft.Azure.WebJobs;

namespace BaltaFunctions
{
    public class ProcessOrderFunction
    {
        private readonly MessageBusService _messageService;
        private readonly NotificationService _notificationService;
        private readonly OrderApiClient _orderClient;

        public ProcessOrderFunction(MessageBusService messageService, NotificationService notificationService, OrderApiClient orderClient)
        {
            _messageService = messageService;
            _notificationService = notificationService;
            _orderClient = orderClient;
        }

        [FunctionName("ProcessOrderFunction")]
        public async Task ProcessOrder([ServiceBusTrigger("orders", Connection = "microsservicos_SERVICEBUS")] Order order, CancellationToken cancellationToken)
        {
            if (order.Customer == "falha@balta.io")
                await _messageService.SendAsync(order, "refund");

            await _orderClient.UpdateAsync(order.Id, new UpdateOrderStatusViewModel
            {
                Status = EOrderStatus.Paid
            }, cancellationToken);

            await _notificationService.NotifyAsync(order.Customer, $"[FUNC] Olá {order.Customer}, seu pedido {order.Number} foi pago e está em separação no estoque!");
        }

        [FunctionName("RefundOrderFunction")]
        public async Task Run([ServiceBusTrigger("refund", Connection = "microsservicos_SERVICEBUS")] Order order, CancellationToken cancellationToken)
            => await _notificationService.NotifyAsync(order.Customer, $"[FUNC] O pagamento do seu pedido {order.Number} no valor de {order.Total} foi estornado!");

        [FunctionName("ShipOrderFunction")]
        public async Task ShipOrder([ServiceBusTrigger("shipping", Connection = "microsservicos_SERVICEBUS")] Order order, CancellationToken cancellationToken)
            => await _notificationService.NotifyAsync(order.Customer, $"[FUNC] Olá {order.Customer}, seu pedido {order.Number} saiu para entrega!");
    }
}
