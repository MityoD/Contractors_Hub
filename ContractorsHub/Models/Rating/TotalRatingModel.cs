using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Models.Rating
{
    public class TotalRatingModel
    {

        [Required]
        public string ContractorId { get; set; } = null!;

        [Required]
        public int TotalRates { get; set; }

        [Required]
        public double TotalPoints { get; set; }

    }
}
