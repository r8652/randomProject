using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.AspNetCore.Mvc;
namespace exe1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomControler
    {

        private readonly IRandomService random_Service;

        public RandomControler(IRandomService randomService)
        {
            random_Service = randomService;
        }
        [HttpGet("TheRandomPrize")]
        public async Task<User> TheRandomPrize(int prizeId)
        {
            // פשוט מחזירים את התוצאה מהסרוויס כמו שהיא
            var winner = await random_Service.TheRandomPrize(prizeId);

            return winner;
        }
        [HttpGet("GetWinnerReport")]
        public async Task <IEnumerable< DtoWinnersReport>> GetWinnerReport()
        {
            return await random_Service.GetWinnerReport();
        }
    }
}
