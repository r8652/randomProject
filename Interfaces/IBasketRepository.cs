using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces
{
    public interface IBasketRepository
    {
        Task AddToBasket(Basket basketItem);

        Task<bool> DeleteFromBasket(int basketId);
        Task<Prize> GetPrizeById(int prizeId);
        Task<IEnumerable<DtoListOrders>> GetPurchasesByPrize(int prizeId);
        Task<IEnumerable<Prize>> TheMostPurchasedPrizes();
        //Task<Basket> GetBasketByUserIdAsync(int userId);
        Task<Basket> GetBasketOfUser(int userId);
        Task SavetheChanges();
    }
}
