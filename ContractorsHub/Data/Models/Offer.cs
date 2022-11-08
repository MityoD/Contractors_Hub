using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public int OwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(Job))]
        public int JobId { get; set; }

        [Required]
        public Job Job { get; set; } = null!;
    }
}
