namespace ContractorsHub.Models.Offer
{
    public class MyOffersViewModel
    {
        public int OfferId { get; set; }

        public string Description { get; set; } = null!;

        public string ContractorName { get; set; } = null!;

        public decimal Price { get; set; }

    }
}
