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
        public int JobId { get; set; }
        
        [StringLength(200)]
        public string Comment { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Points { get; set; }
    }
}
