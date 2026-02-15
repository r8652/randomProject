using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using System.Drawing;

namespace exe1.Services
{
    public class Donor_service : IDonorService
    {
        private readonly IDonorRepository repository;

        public Donor_service(IDonorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Donors>> GetDonors()
        {
            var donors = await repository.GetDonors();
            return donors;
        }

        public async Task<Donors> AddNewDoners(DtoAddDoners dto)
        {
            Donors donor = new Donors();
            donor.Name = dto.Name;
            donor.Email = dto.Email;
            donor.Phone = dto.Phone;
            donor.Last_name = dto.Last_name;

            return await repository.AddNewDoners(donor);
        }

        public async Task<Donors> UpdateDonors(UpdateDonors dto, int id)
        {
            var d = await repository.GetDonorsById(id);
            if (d == null)
            {
                return null;
            }

            d.Name = dto.Name;
            d.Email = dto.Email;
            d.Phone = dto.Phone;
            d.Last_name = dto.Last_name;

            await repository.saveDonors(d);
            return d;
        }
        public async Task<bool> DeleteDonor(int id)
        {
            return await repository.DeleteDonor(id);
        }
        public async Task<IEnumerable<Prize>> GetPrizesByDonorId(int donorId)
        {
            return await repository.GetPrizesByDonorId(donorId);
        }
    }
}