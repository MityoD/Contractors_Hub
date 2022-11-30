using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models.Rating;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository repo;

        public RatingService(IRepository _repo)
        {
            repo = _repo;   
        }

        public async Task<TotalRatingModel> GetRatingAsync(string contractorId)
        {
            double allPoints = 0;

            int ratesCount = 0;           

            var allRatrings = await repo.AllReadonly<Rating>().Where(x => x.ContractorId == contractorId).ToListAsync();

            if (allRatrings.Count > 0)
            {
                foreach (var rate in allRatrings)
                {
                    allPoints += rate.Points;
                    ratesCount++;
                }
            }
            //Contractor name??
            return new TotalRatingModel()
            {
                ContractorId = contractorId,
                TotalPoints = ratesCount == 0? 0 : (double)allPoints/ratesCount,
                TotalRates = ratesCount
            };


        }

        public Task RateAsync(string userId, string contractorId)
        {
            throw new NotImplementedException();
        }
    }
}
