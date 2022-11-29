using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models.Tool
{
    public class ToolModel
    {
       
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> ToolCategories { get; set; } = new List<CategoryViewModel>();

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        //public User Owner { get; set; } = null!;

        [Required]
        //[Range(typeof(decimal),"0.01","9999.99")]
        public int Price { get; set; }
        //price!!!
    }
}
