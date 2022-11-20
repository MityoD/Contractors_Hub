using ContractorsHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models.Offer
{
    public class OfferViewModel
    {   
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string OwnerId { get; set; } = null!;

        [Required]    
        public int JobId { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
