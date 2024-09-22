namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken1 = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);

        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}
