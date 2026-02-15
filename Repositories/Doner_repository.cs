using exe1.data;
using exe1.Interfaces;
using exe1.models;
using Microsoft.EntityFrameworkCore;

namespace exe1.Repositories
{
    public class Doner_repository : IDonorRepository
    {
        private readonly ApiContext context;

        public Doner_repository(ApiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Donors>> GetDonors()
        {
            return await context.Donors.ToListAsync();
        }

        public async Task<Donors> AddNewDoners(Donors donor)
        {
            await context.Donors.AddAsync(donor);
            await context.SaveChangesAsync();
            return donor;
        }

        public async Task<Donors> GetDonorsById(int id)
        {
            return await context.Donors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task saveDonors(Donors donor)
        {
            context.Donors.Update(donor);
            await context.SaveChangesAsync();
        }
        //DeleteDonor
        public async Task<bool> DeleteDonor(int id)
        {
            var donor = await context.Donors.Include(d => d.Prizes).FirstOrDefaultAsync(d => d.Id == id);
            if (donor == null) return false;

            try
            {
                if (donor.Prizes != null && donor.Prizes.Any())
                {
                    context.Prize.RemoveRange(donor.Prizes);
                }

                var basket = await context.Basket.FirstOrDefaultAsync(b => b.User_id == id);

                if (basket != null)
                {
                    var relatedTickets = context.Tickets.Where(t => t.BasketId == basket.Id);
                    if (relatedTickets.Any())
                    {
                        context.Tickets.RemoveRange(relatedTickets);
                    }

                    context.Basket.Remove(basket);
                }

                context.Donors.Remove(donor);

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var realError = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine("SQL ERROR: " + realError);
                throw new Exception(realError);
            }
        }
        public async Task<IEnumerable<Prize>> GetPrizesByDonorId(int donorId)
        {
            return await context.Prize
                .Where(p => p.Doner_id == donorId)
                .ToListAsync();
        }


    }
}