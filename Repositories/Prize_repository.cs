using exe1.data;
using exe1.Interfaces;
using exe1.models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace exe1.Repositories
{
    public class Prize_repository: IPrizeRepozitory
    {
     private readonly ApiContext context;
       public Prize_repository(ApiContext context)
        {
               this.context = context;

        }
        //GetPrize
        public async Task<IEnumerable<Prize>> GetPrize()
        {
            var prizes = await context.Prize.
                Include(x => x.Thewinner).
                ToListAsync();
            return prizes;
        }
        //GetPrizeById
        public async Task<Prize> GetPrizeById(int id)
        {
           var p = await context.Prize.FirstOrDefaultAsync(x => x.Id == id);
            return p;
        }
        //AddPrize
        public async Task<Prize> AddPrize(Prize Prize)
        {

            await context.Prize.AddAsync(Prize);
            await context.SaveChangesAsync();
            return Prize;
        }
        //DeletePrize
        public async Task<bool> DeletePrize(int id)
        {
            var prize = await context.Prize.FindAsync(id);

            if (prize == null)
                return false;

            var relatedTickets = context.Tickets.Where(t => t.PrizeId == id);
            context.Tickets.RemoveRange(relatedTickets);

            context.Prize.Remove(prize);

            await context.SaveChangesAsync();
            return true;
        }
        //UpdatePrize
        public async Task<Prize?> UpdatePrize(Prize p)
        {
            var prize = await context.Prize.FindAsync(p.Id);


            prize.Name = p.Name;
            prize.Descraption = p.Descraption;
            prize.Doner_id = p.Doner_id;
            prize.ImageUrl = p.ImageUrl;
            prize.price = p.price;
            prize.Category = p.Category;

            await context.SaveChangesAsync();
            return prize;
        }
        public async Task<Donors> GetDonorWithPrizes(int id)
        {
            return await context.Donors
                .Include(d => d.Prizes)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
           //GetDonorByPrizeId
          public async Task<Donors>? GetDonorByPrizeId(int prizeId)
        {
            var prize = await context.Prize
           .Include(p => p.Doner)
           .FirstOrDefaultAsync(p => p.Id == prizeId);


            return prize?.Doner;
        }
        public async Task<List<Prize>> GetFilteredPrizes(int? maxPrice, int? categoryId)
        {
            var query = context.Prize.AsQueryable();

            if (maxPrice.HasValue && maxPrice > 0)
            {
                query = query.Where(p => p.price <= maxPrice.Value);
            }

            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(p => p.category_id == categoryId.Value);
            }

            return await query.ToListAsync();
        }
    }

    }

