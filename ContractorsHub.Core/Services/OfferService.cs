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
        /// <summary>
        /// Offer is accepted, job is marked as taken and contractorId is set
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AcceptOfferAsync(int offerId)
        {
            if (await OfferExists(offerId))
            {            
                int jobId = await repo.AllReadonly<JobOffer>().Where(o => o.OfferId == offerId).Select(x => x.JobId).FirstOrDefaultAsync();

                if (jobId == 0)
                {   
                    throw new Exception("Job not found");
                }

                var offer = await GetOfferAsync(offerId);
                offer.IsAccepted = true;

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
        /// <summary>
        /// Declines offer and set IsAccepted to false
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Returns the offer with given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Offer> GetOfferAsync(int id)
        {
            var offer = await repo.All<Offer>()
                .Where(x => x.Id ==id).FirstOrDefaultAsync();
            if (offer == null) 
            {
                throw new Exception("Offer don't exist");
            }
            return offer;
        }        
        /// <summary>
        /// Returns true if offer exist by given Id and false if it don't exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> OfferExists(int id)
        {
            return await repo.AllReadonly<Offer>().AnyAsync(x => x.Id == id);
        }
        /// <summary>
        /// Returns the current condition of the offers for user with Id 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<OfferServiceViewModel>> OffersConditionAsync(string userId)
        {
            var offersCondition =  await repo.AllReadonly<JobOffer>().Where(j => j.Offer.OwnerId == userId && j.Offer.IsActive == true)
                .Select(x => new OfferServiceViewModel()
                {
                    Description = x.Offer.Description,
                    ContractorName = x.Offer.Owner.UserName,
                    JobCategory = x.Job.Category.Name ?? "include category!",
                    JobTitle = x.Job.Title,
                    IsAccepted = x.Offer.IsAccepted,
                    Id = x.Offer.Id,
                    Price = x.Offer.Price,
                    ContractorPhoneNumber = x.Offer.Owner.PhoneNumber,
                    FirstName = x.Offer.Owner.FirstName,
                    LastName = x.Offer.Owner.LastName,
                    JobDescription = x.Job.Description,   
                    JobId = x.JobId,
                    OwnerId = userId,
                    Rating = "Rating"

                }).ToListAsync();

            if (offersCondition == null)
            {
                throw new Exception("JobOffer entity error");
            }

            return offersCondition;
        }
        /// <summary>
        /// Set the offer IsActive to false only for rejected offers
        /// </summary>
        /// <param name="id"></param>
        /// <param name="offerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RemoveOfferAsync(string id, int offerId)
        {
            if (!(await OfferExists(offerId)))
            {
                throw new Exception("Offer don't exist");
            }

            var offer = await repo.GetByIdAsync<Offer>(offerId);

            if (offer.OwnerId != id)
            {
                throw new Exception("User not owner");
            }

            if (offer.IsAccepted == true || offer.IsAccepted == null)
            {
                throw new Exception("This offer can't be deleted");
            }

            offer.IsActive = false;
            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Add JobOffer entity to the given jobId from user with userId
        /// </summary>
        /// <param name="model"></param>
        /// <param name="jobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
