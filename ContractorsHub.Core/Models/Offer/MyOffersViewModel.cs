namespace ContractorsHub.Core.Models.Offer
{
    public class MyOffersViewModel
    {
        public int OfferId { get; set; }

        public string Description { get; set; } = null!;

        public string ContractorName { get; set; } = null!;

        public decimal Price { get; set; }
        //
        public string JobOwnerId { get; set; } = null!;
        
        public string JobOwnerName { get; set; } = null!;

        public bool? IsAccepted { get; set; }
    }
}
