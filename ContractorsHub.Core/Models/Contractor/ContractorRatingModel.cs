using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Core.Models.Contractor
{
    public class ContractorRatingModel
    {
        [Required]
        public string ContractorId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int JobId { get; set; }

        [StringLength(200)]
        public string? Comment { get; set; }

        [Required]
        [Range(1, 5)]
        [Display(Name ="selected stars")]
        public int Points { get; set; }
    }
}
