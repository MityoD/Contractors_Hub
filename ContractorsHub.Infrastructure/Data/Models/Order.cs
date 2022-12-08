using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemsDetails { get; set; } = null!;

        [Required]
        public string ClientId { get; set; } = null!;

        [ForeignKey(nameof(ClientId))]
        public User Client { get; set; } = null!;

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string OrderAdress { get; set; } = null!;

        public bool IsCompleted { get; set; } = false;

        public DateTime ReceivedOn { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}
