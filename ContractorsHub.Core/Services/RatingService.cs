using ContractorsHub.Core.Contracts;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.Core.Models.Rating;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
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

        public async Task RateContractorAsync(string userId, string contractorId, RatingModel model)
        {
            var user = await repo.AllReadonly<User>()
                .Where(x => x.Id == userId).AnyAsync();
            
            var contractor = await repo.AllReadonly<User>()
                .Where(x => x.Id == contractorId).AnyAsync();


            if (!user || !contractor)
            {
                throw new Exception("Invalid Id!");
            }

            var jobExist = await repo.AllReadonly<Job>()
                .Where(x => x.Id == model.JobId)
                .AnyAsync();

            if (!jobExist)
            {
                throw new Exception("Job do not exist!");
            }

            var ratingExist = await repo.AllReadonly<Rating>()
                .Where(x => x.JobId == model.JobId && x.UserId == userId && x.ContractorId == contractorId)
                .AnyAsync();

            if (ratingExist)
            {
                throw new Exception("Job is already rated!");
            }

            if (userId == contractorId)
            {
                throw new Exception("You can not rate yourself!");
            }


            //check user contractor modelstate
            var contractorRating = new Rating()
            { 
                Comment = model.Comment,
                ContractorId = model.ContractorId,
                UserId = model.UserId,
                Points = model.Points,
                JobId = model.JobId,
            };

            await repo.AddAsync(contractorRating);
            await repo.SaveChangesAsync();
        }
    }
}
