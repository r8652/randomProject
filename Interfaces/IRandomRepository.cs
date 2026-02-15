using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces
{
    public interface IRandomRepository
    {
        Task<Prize> findprizeById(int prizeId);
        Task<IEnumerable<Prize>> GetWinnerReport();
        Task SavetheChanges();
        Task<List<Tickets>> TheRandomPrize(int prizeId);
    }
}
