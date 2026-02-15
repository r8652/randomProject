using exe1.models;

namespace exe1.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreatUser(User user);
        Task<User> GetByUserNameAsync(string userName);
    }
}
