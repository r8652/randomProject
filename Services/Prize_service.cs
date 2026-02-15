using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using exe1.Repositories;

namespace exe1.Services
{
    public class Prize_service:IPrizeService
    {
        private readonly IPrizeRepozitory repository;
            public Prize_service(IPrizeRepozitory repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Prize>> GetPrize()
        {
            var prizes = await repository.GetPrize();
            return (IEnumerable<Prize>)prizes;
        }
        public async Task<Prize> GetPrizeById(int id)
        {

            var p= await repository.GetPrizeById(id);
            if (p == null)
            {
                return null;
            }
            return p;
        }

        public async Task<Prize> AddPrize(AddPrize dto)
        {
            var prize = new Prize();
            prize.Doner_id = dto.Doner_id;
            prize.Name = dto.Name;
            prize.Descraption = dto.Descraption;
            prize.price= dto.price;
            prize.category_id = dto.category_id;
            prize.ImageUrl = dto.ImageUrl;
            await repository.AddPrize(prize);
            return prize;
        }
        public async Task<bool> DeletePrize(int id)
        {
            return await repository.DeletePrize(id);
        }
        public async Task<Prize?> UpdatePrize(Prize p)
        {
            return await repository.UpdatePrize(p);
        }
        public async Task<Donors> GetDonorByPrizeId(int prizeId)
        {
            return await repository.GetDonorByPrizeId(prizeId);
        }

        public async Task<List<Prize>> GetFilteredPrizes(int? maxPrice, int? categoryId)
        {
            return await repository.GetFilteredPrizes(maxPrice, categoryId);
        }
    }
}
