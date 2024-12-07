using Marten.Schema;

namespace Basket.Api.Models
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public int TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCart()
        {
            
        }
        public ShoppingCart(string username)
        {
            Username = username;
        }

    }
}
