using System.Collections.Generic;
using BaltaFunctions.Models;

namespace BaltaFunctions.ViewModels
{
    public class CreateOrderViewModel
    {
        public string Customer { get; set; }
        public List<Product> Products { get; set; }
    }
}