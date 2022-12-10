namespace ContractorsHub.Core.Models.Offer
{
    public class OfferServiceViewModel : OfferViewModel
    {
        public int Id { get; set; }

        public string Rating { get; set; } = null!;

        public string ContractorName{ get; set; } = null!;

        public string ContractorPhoneNumber{ get; set; } = null!;

        public string JobTitle { get; set; } = null!;

        public string JobDescription{ get; set; } = null!;

        public string JobCategory { get; set; } = null!;

        public bool? IsAccepted { get; set; }
    }
}
