using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Infrastructure.Data.Models
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
        public int ToolCategoryId { get; set; }

        [ForeignKey(nameof(ToolCategoryId))]
        public ToolCategory Category { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; } = null!;

        public IEnumerable<ToolCart> ToolsCarts { get; set; } = new List<ToolCart>();
    }
}

