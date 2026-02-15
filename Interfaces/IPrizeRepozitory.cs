using exe1.models;

namespace exe1.Interfaces;

public interface IPrizeRepozitory
{
    Task<IEnumerable<Prize>> GetPrize();

    Task<Prize> GetPrizeById(int id);
    Task<Prize> AddPrize(Prize Prize);
    Task<bool> DeletePrize(int id);
    Task<Prize?> UpdatePrize(Prize p);
    Task<Donors>? GetDonorByPrizeId(int prizeId);
    Task<List<Prize>> GetFilteredPrizes(int? maxPrice, int? categoryId);
}
