using Microsoft.EntityFrameworkCore;

namespace exe1.data
{
    public class ProductContaxtFactory
    {
        private const string ConnectionString = "Server=DESKTOP-EUVRB10;Database=215985516_Productdb2;Trusted_Connection=True;TrustServerCertificate=True;";

        public static ApiContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            return new ApiContext(optionsBuilder.Options);
        }

    }
}
