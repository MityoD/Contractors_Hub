using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models
{
    public class AddJobViewModel
    {       
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } //Address     

    }
}
