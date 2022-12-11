
namespace ContractorsHub.Core.Models.Tool
{
    public class ToolViewModel : ToolServiceViewModel
    {

        public int OrderQuantity { get; set; } = 1;
       
        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public decimal TotalPrice { get; set; }

    }
}
