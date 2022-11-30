using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models.Rating
{
    public class RatingModel
    {
        [Required]
        public string ContractorId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Points { get; set; }

        //offer id
    }
}
