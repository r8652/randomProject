using exe1.Dto;
using Microsoft.AspNetCore.Mvc;

namespace exe1.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> Register(RegisterDto regiterDto);

        Task<LoginResponseDto?> AuthenticateAsync(string UserName, string password);
    }
}
