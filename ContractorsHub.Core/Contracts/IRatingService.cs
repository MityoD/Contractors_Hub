using ContractorsHub.Core.Models.Rating;

namespace ContractorsHub.Core.Contracts
{
    public interface IRatingService
    {
        public Task RateContractorAsync(string userId, string contractorId, RatingModel model);

        public Task<TotalRatingModel> GetRatingAsync(string userId);
    }
}
