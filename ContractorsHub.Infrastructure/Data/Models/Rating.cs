using System.ComponentModel.DataAnnotations;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ContractorId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public int JobId { get; set; }

        [StringLength(200)]
        public string Comment { get; set; } = null!;

        [Required]
        [Range(1,5)]
        public int Points { get; set; }

    }
}
