using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Data.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;//Address

        [Required]
        public string OwnerId { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))] 
        public User Owner { get; set; } = null!;
       
        [StringLength(50)]
        public string? OwnerName { get; set; }

        public string? ContractorId { get; set; }

        [Required]
        public bool IsTaken { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public IEnumerable<JobOffer> JobsOffers { get; set; } = new List<JobOffer>();
    }
}
