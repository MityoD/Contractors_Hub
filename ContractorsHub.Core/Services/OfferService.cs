using ContractorsHub.Core.Contracts;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.Core.Models.Offer;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
{
    public class OfferService : IOfferService
    {
        private readonly IRepository repo;

        public OfferService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AcceptOfferAsync(int offerId)
        {
            if (await OfferExists(offerId))
            {
                var offer = await GetOfferAsync(offerId);
                offer.IsAccepted = true;

                int? jobId = await repo.AllReadonly<JobOffer>().Where(o => o.OfferId == offerId).Select(x => x.JobId).FirstOrDefaultAsync();

                if (jobId == null)
                {   
                    throw new Exception("Invalid job Id");
                }

                var job = await repo.GetByIdAsync<Job>(jobId);
                job.ContractorId = offer.OwnerId;
                job.IsTaken = true;

                await repo.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Offer don't exist");
            }
        }

        public async Task DeclineOfferAsync(int offerId)
        {
            if (await OfferExists(offerId))
            {
                var offer = await GetOfferAsync(offerId);
                offer.IsAccepted = false;
                await repo.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Offer don't exist");
            }
        }

        public async Task<Offer> GetOfferAsync(int id)
        {
            var offer = await repo.GetByIdAsync<Offer>(id);
            if (offer == null) 
            {
                throw new Exception("Offer id not valid");
            }
            return offer;
        }        

        public async Task<bool> OfferExists(int id)
        {
            return await repo.AllReadonly<Offer>().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<OfferServiceViewModel>> OffersConditionAsync(string userId)
        {
            var offersCondition =  await repo.AllReadonly<JobOffer>().Where(j => j.Offer.OwnerId == userId && j.Offer.IsActive == true)
                .Select(x => new OfferServiceViewModel()
                {
                    Description = x.Offer.Description,
                    ContractorName = x.Offer.OwnerId,
                    JobCategory = x.Job.Category.Name ?? "include category!",
                    JobTitle = x.Job.Title,
                    IsAccepted = x.Offer.IsAccepted,
                    Id = x.Offer.Id,
                    Price = x.Offer.Price

                }).ToListAsync();

            if (offersCondition == null)
            {
                throw new Exception("JobOffer entity error");
            }

            return offersCondition;
        }

        public async Task RemoveOfferAsync(string id, int offerId)
        {
            if (!(await OfferExists(offerId)))
            {
                throw new Exception("Offer don't exist");
            }

            var offer = await repo.GetByIdAsync<Offer>(offerId);

            if (offer.OwnerId != id)
            {
                throw new Exception("Owner id error");
            }

            if (offer.IsAccepted == true || offer.IsAccepted == null)
            {
                throw new Exception("This offer can't be deleted");
            }

            offer.IsActive = false;
            await repo.SaveChangesAsync();
        }

        public async Task SendOfferAsync(OfferViewModel model, int jobId, string userId)
        {           
            var job = await repo.GetByIdAsync<Job>(jobId);
            if (job == null)
            {
                throw new Exception("Invalid job Id");
            }

            var userOfferExist = await repo.AllReadonly<JobOffer>()
                .Where(x => x.Offer.OwnerId == userId 
                && x.JobId == jobId 
                && x.Offer.IsAccepted != false)
                .AnyAsync();

            if (userOfferExist)
            {
                throw new Exception("One offer per job");
            }

            
            //check if offer already exist
            var offer = new Offer()
            {
                Description = model.Description,
                OwnerId = userId,
                Price = model.Price

            };
            await repo.AddAsync<Offer>(offer);

            var jobOffer = new JobOffer()
            {
                JobId = job.Id,
                Job = job,
                Offer = offer,
                OfferId = offer.Id
            };

            await repo.AddAsync<JobOffer>(jobOffer);

            await repo.SaveChangesAsync();
           
        }
    }
}
