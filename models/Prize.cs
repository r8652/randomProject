using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exe1.models
{
    public class Prize
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "חובה להזין שם למתנה")]
        public string Name { get; set; }

        public string Descraption { get; set; } 

        public ICollection<Basket>? Owoners { get; set; }

        [Required]
        public int Doner_id { get; set; }
        public Donors? Doner { get; set; }

        public int price { get; set; }

        [Required]
        public int category_id { get; set; }
        public Category? Category { get; set; }

        [Url(ErrorMessage = "כתובת התמונה חייבת להיות קישור תקין")]
        public string? ImageUrl { get; set; }

        public int PurchacesAmount { get; set; }

        public User? Thewinner { get; set; } = null;

        public bool IsActive { get; set; } = true;
        //אם הממנצח אינו NULL אז הפרס כבר הוגרל וא"א לזכותו שוב

    }
}