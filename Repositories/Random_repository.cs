using exe1.data;
using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.EntityFrameworkCore;

namespace exe1.Repositories
{
    public class Random_repository : IRandomRepository
    {
        private readonly ApiContext context;
        public Random_repository(ApiContext context)
        {
            this.context = context;

        }

        public async Task<List<Tickets>> TheRandomPrize(int prizeId)
        {
            // השאילתה הזו מוצאת את כל הכרטיסים ששולמו (שיש להם תאריך)
            var tickets = await context.Tickets
                .Include(t => t.User) // חשוב מאוד כדי שה-Service ידע מי המשתמש
                .Where(t => t.PrizeId == prizeId && t.date > DateTime.MinValue)
                .ToListAsync();

            return tickets;
        }

        public async Task<Prize> findprizeById(int prizeId)
        {
            return await context.Prize.FirstOrDefaultAsync(p => p.Id == prizeId);
        }

        public async Task<IEnumerable<Prize>> GetWinnerReport()
        {
            return await context.Prize
                .Include(p => p.Thewinner)
                .ToListAsync();
           
        }

        public async Task SavetheChanges()
        {
           await context.SaveChangesAsync();
        }

       
    }
}
