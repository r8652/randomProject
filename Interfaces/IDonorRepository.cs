using exe1.Dto;
using exe1.models;

namespace exe1.Interfaces;

public interface IDonorRepository
{
    Task<IEnumerable<Donors>> GetDonors();

    Task<Donors> AddNewDoners(Donors donor);

    Task <Donors>GetDonorsById(int id);
    Task saveDonors(Donors d);
    Task<bool> DeleteDonor(int id);
    Task<IEnumerable<Prize>> GetPrizesByDonorId(int donorId);
}

