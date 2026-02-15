using System.ComponentModel.DataAnnotations;

namespace exe1.Dto
{
    public class PrizeDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descraption { get; set; }

        public string ImageUrl { get; set; }

        public int price { get; set; }

    }


    public class AddPrize
    {

        [Required]
        public string Name { get; set; }
        public string Descraption { get; set; }

        public int Doner_id { get; set; }

        public int category_id { get; set; }
        public int price { get; set; }

        public string ImageUrl { get; set; }

    }
}
