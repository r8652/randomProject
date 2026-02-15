using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces
{
    public interface IRandomService
    {
        Task<IEnumerable<DtoWinnersReport>> GetWinnerReport();
        Task<User> TheRandomPrize(int prizeId);
    }
}
