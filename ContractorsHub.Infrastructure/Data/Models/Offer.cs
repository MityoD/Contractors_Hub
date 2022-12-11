using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; }  = null!; 

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        public bool? IsAccepted { get; set; } = null;

        public IEnumerable<JobOffer> JobsOffers { get; set; } = new List<JobOffer>();

        [Required]
        public bool IsActive { get; set; } = true;

    }
}
