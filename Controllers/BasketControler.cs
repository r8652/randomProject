using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace exe1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase 
    {
        private readonly IBasketService basket_Service;

        public BasketController(IBasketService basketService)
        {
            basket_Service = basketService;
        }

        [HttpGet("GetPurchasesByPrize/{prizeId}")]
        //GetPurchasesByPrize
        public async Task<ActionResult<IEnumerable<DtoListOrders>>> GetPurchasesByPrize(int prizeId)
        {
            var results = await basket_Service.GetPurchasesByPrize(prizeId);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
        [HttpGet("TheMostPurchasedPrizes")]
        //TheMostPurchasedPrizes
        public async Task<ActionResult<Prize>> TheMostPurchasedPrizes()
        {
            var result = await basket_Service.TheMostPurchasedPrizes();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("TheMostExpensivePrize")]

        public async Task<Prize> theMostExpensivePrize()
        {
           
            return await basket_Service.theMostExpensivePrize();
        }
        [HttpDelete("{id}/DeleteItem")]
        //DeleteItem
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await basket_Service.RemoveItemFromBasket(id);

            if (!result)
            {
                return NotFound(new { message = "הפריט לא נמצא בסל" });
            }

            return Ok(new { message = "הפריט הוסר בהצלחה מהסל" });
        }
        [HttpPost("AddToBasket/{prizeId}")]
        public async Task<IActionResult> AddToBasket(int prizeId)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type.Contains("nameidentifier"));

            if (userIdClaim == null)
            {
                return Unauthorized("לא הצלחתי למצוא את ה-ID גם בתוך הטוקן הגולמי.");
            }

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                await basket_Service.AddItemToBasketAsync(userId, prizeId);
                return Ok(new { message = "הצלחה!", userId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetMyBasket")]
        public async Task<IActionResult> GetMyBasket()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                var token = authHeader.Replace("Bearer", "").Trim();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c =>
                    c.Type == "sub" ||
                    c.Type.EndsWith("nameidentifier") ||
                    c.Type == "id");

                if (userIdClaim == null)
                    return Unauthorized("לא נמצא מזהה משתמש בטוקן.");

                int userId = int.Parse(userIdClaim.Value);

                var basketItems = await basket_Service.GetBasketByUserIdAsync(userId);

                return Ok(basketItems);
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה בשליפת הסל: {ex.Message}");
            }
        }
        [HttpPost("CompleteOrder")]
        [Authorize]
        public async Task<IActionResult> CompleteOrder()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                           ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                }
            }

            // בדיקת סיום
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { message = "שגיאה קריטית: הטוקן תקין אבל לא נמצא בתוכו מזהה משתמש (sub)" });
            }

            int userId = int.Parse(userIdClaim);

            // קריאה לשירות שלך
            var result = await basket_Service.CompleteOrder(userId);
            return Ok(new { message = result });
        }

    }
}