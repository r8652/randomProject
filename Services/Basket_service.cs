using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using exe1.Repositories;
using Microsoft.EntityFrameworkCore;

namespace exe1.Services
{
    public class Basket_service : IBasketService
    {
        private readonly IBasketRepository repository;

        public Basket_service(IBasketRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<DtoListOrders>> GetPurchasesByPrize(int prizeId)
        {
            return await repository.GetPurchasesByPrize(prizeId);
        }
        public async Task<Prize> TheMostPurchasedPrizes()
        {
            var prizes = await repository.TheMostPurchasedPrizes();
            var TheMostPurchasedPrizes = new Prize();
            foreach (var item in prizes)
            {
                if (item.PurchacesAmount > TheMostPurchasedPrizes.PurchacesAmount)
                {
                    TheMostPurchasedPrizes = item;
                }
            }
            return TheMostPurchasedPrizes;
        }
        //theMostExpensivePrize
        public async Task<Prize> theMostExpensivePrize()
        {
            var prizes = await repository.TheMostPurchasedPrizes();
            var theMostExpensivePrize = new Prize();
            foreach (var item in prizes)
            {
                if (item.price > theMostExpensivePrize.price)
                {
                    theMostExpensivePrize = item;
                }
            }
            return theMostExpensivePrize;
        }
        //
        public async Task<bool> RemoveItemFromBasket(int basketId)
        {
            return await repository.DeleteFromBasket(basketId);
        }

        public Task<bool> DeleteFromBasket(int basketId)
        {
            throw new NotImplementedException();
        }
        public async Task AddItemToBasketAsync(int userId, int prizeId)
        {
            var basketItem = await repository.GetBasketOfUser(userId);

            if (basketItem == null)
            {
                var prize = await repository.GetPrizeById(prizeId);
                if (prize == null) throw new Exception("הפרס לא נמצא");

                basketItem = new Basket
                {
                    User_id = userId,
                    Prize = prize
                };

                await repository.AddToBasket(basketItem);
            }

            var newTicket = new Tickets
            {
                UserId = userId,
                PrizeId = prizeId,
                BasketId = basketItem.Id, // שימי לב אם זה id או Id במודל שלך
                date = DateTime.MinValue
            };

            if (basketItem.YourTickets == null) basketItem.YourTickets = new List<Tickets>();
            basketItem.YourTickets.Add(newTicket);

            await repository.SavetheChanges();
        }
        public async Task<Basket> GetBasketByUserIdAsync(int userId)
        {
            return await repository.GetBasketOfUser(userId); 
        }
        public async Task<string> CompleteOrder(int userId)
        {
            var basket = await repository.GetBasketOfUser(userId);

            if (basket == null || basket.YourTickets == null || !basket.YourTickets.Any())
            {
                return "הסל ריק";
            }

            foreach (var ticket in basket.YourTickets)
            {
                if (ticket.Prize != null)
                {
                    ticket.Prize.PurchacesAmount += 1;
                }

               
                ticket.date = DateTime.Now;
            }

            await repository.SavetheChanges();

            return "ההזמנה הושלמה";
        }
    }
}