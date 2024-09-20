using Basket.Api.Data.Entities;

namespace Basket.Api.Data.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string userName);
}