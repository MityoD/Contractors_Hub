using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Data.Models
{
    public class Tool
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [Range(1,1000)]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public ToolCategory Category { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner{ get; set; }

        public bool IsActive { get; set; } = true;

    }
}
