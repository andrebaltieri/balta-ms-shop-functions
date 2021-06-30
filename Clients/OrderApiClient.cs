using System.Threading;
using System.Threading.Tasks;
using BaltaFunctions.Models;
using BaltaFunctions.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace BaltaFunctions.Clients
{
    public class OrderApiClient
    {
        public async Task UpdateAsync(int orderId, UpdateOrderStatusViewModel model, CancellationToken cancellationToken)
        {
            var client = new RestClient($"https://balta-ms-shop-orders.azurewebsites.net/v1/orders/{orderId}");
            var request = new RestRequest(JsonConvert.SerializeObject(model), DataFormat.Json);

            await client.PutAsync<Order>(request, cancellationToken);
        }
    }
}
