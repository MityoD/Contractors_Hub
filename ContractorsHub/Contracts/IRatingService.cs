using ContractorsHub.Models.Rating;

namespace ContractorsHub.Contracts
{
    public interface IRatingService
    {
        public Task RateAsync(string userId, string contractorId);

        public Task<TotalRatingModel> GetRatingAsync(string userId);
    }
}
