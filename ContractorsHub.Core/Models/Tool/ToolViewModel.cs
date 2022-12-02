
namespace ContractorsHub.Core.Models.Tool
{
    public class ToolViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public int Quantity { get; set; }
       
        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string OwnerName { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
