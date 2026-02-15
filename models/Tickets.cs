namespace exe1.models
{
    public class Tickets
    {
        public int id { get; set; }

        public DateTime date { get; set; }
        public Prize? Prize { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int? BasketId { get; set; }

        public int PrizeId { get; set; }
    }
}
