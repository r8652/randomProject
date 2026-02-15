using System.ComponentModel.DataAnnotations.Schema;

namespace exe1.models
{
    public class Basket
    {
        public int Id { get; set; }

        public int? User_id { get; set; }

        [ForeignKey("User_id")]
        public virtual User User { get; set; }

        public ICollection<Tickets> YourTickets { get; set; }

        public int PrizeId { get; set; } 
        public Prize? Prize { get; set; } 

    }
}
