
namespace Basket.Api.Data
{
    public interface ICachedBasketRepository
    {
        Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
        Task<ShoppingCart> GetBasket(string username, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    }
}