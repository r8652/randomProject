using exe1.Dto;
using exe1.models;
using Microsoft.AspNetCore.Mvc;

namespace exe1.Interfaces
{
    public interface IBasketService
    {

        Task<IEnumerable<DtoListOrders>> GetPurchasesByPrize(int prizeId);


        Task<Prize> TheMostPurchasedPrizes();

        Task<Prize> theMostExpensivePrize();
        Task<bool> DeleteFromBasket(int basketId);
        Task<bool> RemoveItemFromBasket(int id);
        Task AddItemToBasketAsync(int userId, int prizeId);
        Task<Basket> GetBasketByUserIdAsync(int userId);     
         Task<string> CompleteOrder(int userId);
    }
}