using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exe1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService User_Service;

        public UserController(IUserService User_Service)
        {
            this.User_Service = User_Service;

        }
        [HttpPost("Register")]
        public async Task<UserResponseDto> Register(RegisterDto regiterDto)
        {
            return await User_Service.Register(regiterDto);
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (string.IsNullOrWhiteSpace(loginRequestDto.UserName) || string.IsNullOrWhiteSpace(loginRequestDto.Password))
            {
                return BadRequest(new { message = "UserName and password are required." });
            }

            var result = await User_Service.AuthenticateAsync(loginRequestDto.UserName, loginRequestDto.Password);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid UserName or password." });
            }

            return Ok(result);
        }
    }
}