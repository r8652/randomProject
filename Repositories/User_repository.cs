using exe1.data;
using exe1.Dto;
using exe1.Interfaces;
using exe1.models;
using Microsoft.EntityFrameworkCore;

namespace exe1.Repositories
{
    public class User_repository: IUserRepository
    {
        private readonly ApiContext context;
        public User_repository(ApiContext context)
        {
            this.context = context;

        }
        public async Task <User> CreatUser(User user)
        {
            context.User.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetByUserNameAsync(string UserName)
        {
            return await context.User
            .FirstOrDefaultAsync(u => u.userName == UserName);
        }

       
    }
}
