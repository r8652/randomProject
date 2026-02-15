using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using exe1.Services;
using Microsoft.AspNetCore.Mvc;

namespace exe1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorControler : ControllerBase

        {   
        private readonly IDonorService Donor_service;

        public DonorControler(IDonorService donorService)
        {
            Donor_service = donorService;

        }
        [HttpGet("GetDonors")]
        public async Task<IActionResult> GetDonors()
        {
            var donors = await Donor_service.GetDonors();
            return Ok(donors);
        }
        [HttpPost("AddNewDoners")]
        public async Task<Donors> AddNewDoners(DtoAddDoners dto)
        {
            return await Donor_service.AddNewDoners(dto);
        }
        [HttpPut("UpdateDonors/{id}")]
        public async Task <Donors> UpdateDonors(int id, UpdateDonors dto)
        {
            return await Donor_service.UpdateDonors(dto, id);
        }
        [HttpDelete("DeleteDonor/{id}")]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            try
            {
                var success = await Donor_service.DeleteDonor(id);
                if (!success)
                {
                    return NotFound("התורם לא נמצא במערכת");
                }
                return Ok("התורם נמחק בהצלחה");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"שגיאת שרת פנימית: {ex.Message}");
            }
        }
        [HttpGet("GetDonorPrizesById/{id}")]
        public async Task<ActionResult<IEnumerable<Prize>>> GetDonorPrizes(int id)
        {
            var prizes = await Donor_service.GetPrizesByDonorId(id);

            
            if (prizes == null || !prizes.Any())
            {
                return Ok(new List<Prize>());
            }

            return Ok(prizes);
        }

    }
}