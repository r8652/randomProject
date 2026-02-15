using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using exe1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exe1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : ControllerBase
    {
        private readonly IPrizeService prize_Service;

        public PrizeController(IPrizeService prizeService)
        {
            prize_Service = prizeService;
        }

        [HttpGet("GetListPrize")]
        public async Task<ActionResult> GetPrize()
        {
            var prize = await prize_Service.GetPrize();
            return Ok(prize);
        }

        [HttpGet("GetPriceById")]
        public async Task<Prize> GetPrizeById(int id)
        {
            return await prize_Service.GetPrizeById(id);
        }

        [HttpPost("AddPrize")]
        public async Task<Prize> AddPrize(exe1.Dto.AddPrize dto)
        {
            var addPrize = new AddPrize
            {
                Name = dto.Name,
                Descraption = dto.Descraption,
                Doner_id = dto.Doner_id,
                category_id = dto.category_id,
                price = dto.price,
                ImageUrl=dto.ImageUrl
            };
            return await prize_Service.AddPrize(addPrize);
        }

        [HttpDelete("DeletePrize/{id}")]
        public async Task<bool> DeletePrize(int id)
        {
            return await prize_Service.DeletePrize(id);
        }

        [HttpPut("UpdatePrize/{id}")]
        public async Task<IActionResult> UpdatePrize(int id, PrizeDto dto)
        {
            // 1. קודם כל, ניגשים ל-Service כדי לקבל את האובייקט האמיתי מהמסד נתונים
            // (הנחה שיש לך פונקציה ב-Service שמביאה לפי ID, אם לא - השתמשי ב-Context ישירות)
            var p = await prize_Service.UpdatePrize(new Prize
            {
                Id = id,
                Name = dto.Name,
                Descraption = dto.Descraption,
                ImageUrl = dto.ImageUrl,
                price = dto.price 
            });

            if (p == null) return NotFound("הפרס לא נמצא או שהעדכון נכשל");

            var resultDto = new PrizeDto
            {
                Id = p.Id,
                Name = p.Name,
                Descraption = p.Descraption,
                ImageUrl = p.ImageUrl,
                price = p.price
            };

            return Ok(resultDto);
        }
        [HttpGet("GetDonorByPrizeId/{prizeId}")]
        public async Task<ActionResult<Donors>> GetDonorByPrizeId(int prizeId)
        {
            var donor = await prize_Service.GetDonorByPrizeId(prizeId);

            if (donor == null)
            {
                return NotFound($"לא נמצא תורם עבור מתנה עם מזהה {prizeId}");
            }

            return Ok(donor);
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Prize>>> GetByFilters(int? price, int? category)
        {
            var result = await prize_Service.GetFilteredPrizes(price, category);
            return Ok(result);
        }
    }
}