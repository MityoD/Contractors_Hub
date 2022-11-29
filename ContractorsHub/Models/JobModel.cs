using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models
{
    public class JobModel
    {       
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> JobCategories { get; set; } = new List<CategoryViewModel>();

        [Required]
        [StringLength(500)]
        public string Description { get; set; } //Address     

        public User? Owner { get; set; } //= null!;

        public string? OwnerName { get; set; } //= null!;


    }
}
