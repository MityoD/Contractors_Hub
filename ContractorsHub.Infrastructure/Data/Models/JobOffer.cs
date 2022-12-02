using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class JobOffer
    {
        [Required]
        public int JobId { get; set; }
        
        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; } = null!;

        [Required]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; } = null!;
    }
}
