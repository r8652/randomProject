using exe1.models;

namespace exe1.Dto
{
    public class DtoBasket
    {
      
    }
    public class DtoListOrders
    {
       public Prize? prize { get; set; }

        public User? User { get; set; }
        public int TicketAmount { get; set; }
       
        public DateTime date { get; set; }
    }
}
