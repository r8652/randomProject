using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces;

public interface IDonorService
{
    Task<IEnumerable<Donors>> GetDonors();

    Task<Donors> AddNewDoners(DtoAddDoners dto);
    Task<Donors> UpdateDonors(UpdateDonors dto, int id);
    Task<bool> DeleteDonor(int id);
    Task<IEnumerable<Prize>> GetPrizesByDonorId(int donorId);
}
