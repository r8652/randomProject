using exe1.models;
using Microsoft.EntityFrameworkCore;

namespace exe1.data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<Category> Category => Set<Category>();
        public DbSet<User> User => Set<User>();

        public DbSet <Basket> Basket => Set<Basket>();
        public DbSet<Donors> Donors => Set<Donors>();
        public DbSet <Prize> Prize => Set<Prize>();
        public DbSet<Tickets> Tickets => Set<Tickets>();


    }
}
