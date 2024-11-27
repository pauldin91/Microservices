﻿using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string username,CancellationToken cancellationToken=default);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart,CancellationToken cancellationToken=default);
        Task<bool> DeleteBasket(string username, CancellationToken cancellationToken=default);
    }
}
