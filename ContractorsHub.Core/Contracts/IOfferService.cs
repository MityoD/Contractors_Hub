using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.Core.Models.Offer;

namespace ContractorsHub.Core.Contracts
{
    public interface IOfferService
    {
        Task SendOfferAsync(OfferViewModel model, int jobId, string userId);

        //Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId);

        Task<IEnumerable<OfferServiceViewModel>> OffersConditionAsync(string userId);

        Task<bool> OfferExists(int id);

        Task<Offer> GetOfferAsync(int id);
        
        Task AcceptOfferAsync(int offerId);

        Task DeclineOfferAsync(int offerId);

        Task RemoveOfferAsync(string id, int offerId);
    }
}
