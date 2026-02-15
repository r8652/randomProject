using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace exe1.models
{
    public class Category
    {       
        public int Id { get; set; }

        [MinLength(3)]
        public string? name { get; set; }
        //[ForeignKey("menegerId")]
       // public int menegerId {  get; set; }
        //public Maneger?maneger { get; set; }
        public ICollection<Prize>? Prizes { get; set; } = new List<Prize>();
       
    }
}
