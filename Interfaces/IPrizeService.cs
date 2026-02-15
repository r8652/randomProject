using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces;

public interface IPrizeService
{
    Task<IEnumerable<Prize>> GetPrize();
    Task<Prize> GetPrizeById(int id);
    Task<Prize> AddPrize(AddPrize dto);
    Task<bool> DeletePrize(int id);
    Task<Prize> UpdatePrize(Prize prize);
    Task<Donors>? GetDonorByPrizeId(int prizeId);
    Task<List<Prize>> GetFilteredPrizes(int? maxPrice, int? categoryId);
}