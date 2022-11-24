using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models.Offer;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Services
{
    public class OfferService : IOfferService
    {
        private readonly IRepository repo;

        public OfferService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AcceptOfferAsync(Offer offer)
        {
            offer.IsAccepted = true;
            await repo.SaveChangesAsync();
        }

        public async Task DeclineOfferAsync(Offer offer)
        {
            offer.IsAccepted = false;
            await repo.SaveChangesAsync();
        }

        public async Task<Offer> GetOfferAsync(int id)
        {
            //if (await OfferExists(id))
            //{
               
            //}
            return await repo.GetByIdAsync<Offer>(id);
        }

        public  async Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId)
        {
            return await repo.AllReadonly<JobOffer>().Where(j => j.Job.OwnerId == userId)
                .Select(x => new MyOffersViewModel()
            {
                Description = x.Offer.Description,
                ContractorName = x.Offer.OwnerId,
                OfferId = x.Offer.Id

            }).ToListAsync();

            

            //
            //return .Select(async x => new MyOffersViewModel()
            //{
            //    Description = x.Offer.Description,
            //    ContractorName = "Name"


            //}).ToListAsync();
        }

        public async Task<bool> OfferExists(int id)
        {
            return await repo.AllReadonly<Offer>().AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MyOffersViewModel>> OffersConditionAsync(string userId)
        {
            return await repo.AllReadonly<JobOffer>().Where(j => j.Offer.OwnerId == userId)
                .Select(x => new MyOffersViewModel()
                {
                    Description = x.Offer.Description,
                    ContractorName = x.Offer.OwnerId,
                    JobOwnerId = x.Job.Owner.Id,
                    JobOwnerName = x.Job.Owner.UserName,
                    IsAccepted = x.Offer.IsAccepted,
                    OfferId = x.Offer.Id

                }).ToListAsync();
        }
            /*repo.AllReadonly<User>().Where(u => u.Id == x.Offer.OwnerId).Select(x => x.UserName)*/// add name to offer?
            public async Task SendOfferAsync(OfferViewModel model, int jobId, string userId)
        {
           
                // check model state?
                //check if offer already exist
                var job = await repo.GetByIdAsync<Job>(jobId);
                var user = await repo.GetByIdAsync<User>(userId);
                var offer = new Offer()
                {
                    Description = model.Description,
                    OwnerId = userId,
                   // Owner = user,
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
