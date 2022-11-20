using ContractorsHub.Data.Models;
using ContractorsHub.Models.Offer;

namespace ContractorsHub.Contracts
{
    public interface IOfferService
    {
        Task SendOfferAsync(OfferViewModel model, int jobId, string userId);

        Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId);

        Task<IEnumerable<MyOffersViewModel>> OffersConditionAsync(string userId);

        Task <OfferServiceViewModel> ReviewOfferAsync(int id);

        Task<bool> OfferExists(int id);

        Task<Offer> GetOfferAsync(int id);
        
        Task AcceptOfferAsync(Offer offer);

        Task DeclineOfferAsync(Offer offer);
    }
}
