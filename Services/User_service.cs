using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using exe1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace exe1.Services
{
    public class User_service : IUserService
    {
        private readonly IUserRepository repository;
        private readonly ITokenService tokenService;
        private readonly IConfiguration _configuration;

        public User_service(IUserRepository repository, ITokenService tokenService, IConfiguration configuration)
        {
            this.repository = repository;
            this.tokenService = tokenService;
            this._configuration = configuration;
        }

        // --- Register ---
        public async Task<UserResponseDto> Register(RegisterDto registerDto)
        {
            var user = new User();
            user.name = registerDto.Name;
            user.userName = registerDto.userName;
            user.Phone = registerDto.Phone;
            user.Email = registerDto.Email;
            user.pasword = HashPassword(registerDto.Password);

            user.Role = !string.IsNullOrEmpty(registerDto.Role) ? registerDto.Role : "User";

            await repository.CreatUser(user);

            return new UserResponseDto()
            {
                Id = user.Id,
                Name = user.name,
                UserName = user.userName,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        // --- Authenticate (Login) - מעודכן ללא Throw ---
        public async Task<LoginResponseDto?> AuthenticateAsync(string UserName, string password)
        {
            var user = await repository.GetByUserNameAsync(UserName);

            // אם המשתמש לא נמצא, מחזירים null במקום לזרוק שגיאה
            if (user == null)
            {
                return null;
            }

            var hashedPassword = HashPassword(password);
            // אם הסיסמה לא תואמת, מחזירים null
            if (user.pasword != hashedPassword)
            {
                return null;
            }

            // יצירת טוקן
            var token = tokenService.GenerateToken(user.Id, user.Email, user.name, user.Role ?? "User");

            var expiryMinutesString = _configuration["JwtSettings:ExpiryMinutes"];
            if (!int.TryParse(expiryMinutesString, out int expiryMinutes))
            {
                expiryMinutes = 60;
            }

            var userResponseDto = new UserResponseDto()
            {
                Id = user.Id,
                Name = user.name,
                UserName = user.userName,
                Email = user.Email,
                Phone = user.Phone
            };

            return new LoginResponseDto
            {
                Token = token,
                TokenType = "Bearer",
                ExpiresIn = expiryMinutes * 60,
                User = userResponseDto,
                Role = user.Role
            };
        }

        private static string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        // מימוש הפונקציה מהממשק אם צריך, או להשאיר כך אם היא לא בשימוש
        public Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}