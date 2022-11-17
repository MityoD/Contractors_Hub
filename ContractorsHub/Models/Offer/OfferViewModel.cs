using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models.Offer
{
    public class OfferViewModel
    {
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public int OwnerId { get; set; }

        [Required]    
        public int JobId { get; set; }

    }
}
