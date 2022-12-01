using ContractorsHub.Models.Rating;

namespace ContractorsHub.Contracts
{
    public interface IRatingService
    {
        public Task RateContractorAsync(string userId, string contractorId, RatingModel model);

        public Task<TotalRatingModel> GetRatingAsync(string userId);
    }
}
