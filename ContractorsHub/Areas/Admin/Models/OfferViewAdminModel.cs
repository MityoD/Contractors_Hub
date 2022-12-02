using ContractorsHub.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Areas.Administration.Models
{
    public class OfferViewAdminModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(OwnerId))]
        public List<User> Owner { get; set; } = new List<User>();

        [Required]
        public string OwnerId { get; set; } //null!; 
                                             // or int and add contractor entity
        [Required]
        public decimal Price { get; set; }
        //time

        public bool? IsAccepted { get; set; } = null;

        public IEnumerable<JobOffer> JobsOffers { get; set; } = new List<JobOffer>();
    }
}
