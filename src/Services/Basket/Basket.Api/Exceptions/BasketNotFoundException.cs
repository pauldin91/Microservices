﻿using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {

        public BasketNotFoundException(string? usernmae) : base("Basket",usernmae)
        {
        }
    }
}