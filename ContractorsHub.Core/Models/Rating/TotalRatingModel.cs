using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Core.Models.Rating
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
