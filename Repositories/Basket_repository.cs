using exe1.data;
using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.EntityFrameworkCore;

namespace exe1.Repositories
{
    public class Basket_repository : IBasketRepository
    {
        private readonly ApiContext context;

        public Basket_repository(ApiContext context)
        {
            this.context = context;
        }

        //GetPurchasesByPrize
        public async Task<IEnumerable<DtoListOrders>> GetPurchasesByPrize(int prizeId)
        {
            var purchases = await context.Tickets
                .Where(t => t.Prize.Id == prizeId)
                .Select(t => new DtoListOrders
                {
                    prize = t.Prize,
                    User = t.User,
                    TicketAmount = 1,
                    date = t.date
                })
                .ToListAsync(); 

            return purchases;
        }
        //TheMostPurchasedPrizes
        public async Task<IEnumerable<Prize>> TheMostPurchasedPrizes()
        {
            var result = await context.Prize.ToListAsync();
            return result;
        }
        //DeleteFromBasket
        public async Task<bool> DeleteFromBasket(int basketId)
        {
            var relatedTickets = context.Tickets.Where(t => t.BasketId == basketId);
            context.Tickets.RemoveRange(relatedTickets);

            var basketItem = await context.Basket.FindAsync(basketId);
            if (basketItem == null) return false;

            context.Basket.Remove(basketItem);

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<Prize> GetPrizeById(int prizeId)
        {
            return await context.Prize.FindAsync(prizeId);
        }

        public async Task AddToBasket(Basket item)
        {
            await context.Basket.AddAsync(item);
            await context.SaveChangesAsync();
        }

        //public async Task<Basket> GetBasketByUserIdAsync(int userId)
        //{
        //    return await context.Basket
        //        .Include(b => b.YourTickets)
        //            .ThenInclude(t => t.Prize)
        //        .FirstOrDefaultAsync(b => b.User_id == userId);
        //}
        public async Task SavetheChanges()
        {
            await context.SaveChangesAsync();
        }
        public async Task<List<Tickets>> GetUnpaidTicketsByUserId(int userId)
        {
            return await context.Tickets
                .Where(t => t.UserId == userId && t.date == DateTime.MinValue)
                .ToListAsync();
        }

        public async Task<Basket> GetBasketOfUser(int userId)
        {
            {
                // הקוד המנצח שלנו עם ה-Include
                return await context.Basket
                    .Include(b => b.YourTickets)
                        .ThenInclude(t => t.Prize)
                    .FirstOrDefaultAsync(b => b.User_id == userId);
            }
        }

        //public Task<Basket> GetBasketByUserIdAsync(int userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
