using System.Diagnostics;

namespace Basket.Api.Models
{
    public class ShoppingCartItem
    {
        public Guid ProductId { get; set; } = default!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } = default!;
        public string Color { get; set; } = default!;
    }
}