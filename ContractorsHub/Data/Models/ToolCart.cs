using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorsHub.Data.Models
{
    public class ToolCart
    {
        [Required]
        public int CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

        [Required]
        public int ToolId { get; set; }

        [ForeignKey(nameof(ToolId))]
        public Tool Tool { get; set; } = null!;
    }
}
