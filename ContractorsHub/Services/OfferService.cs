using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models.Offer;
using ContractorsHub.Data.Models;
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

        public  async Task<IEnumerable<MyOffersViewModel>> MyOffersAsync(string userId)
        {
            return await repo.AllReadonly<JobOffer>().Where(j => j.Job.OwnerId == userId)
                .Select(x => new MyOffersViewModel()
            {
                Description = x.Offer.Description,
                ContractorName = x.Offer.OwnerId
            }).ToListAsync();

            

            //
            //return .Select(async x => new MyOffersViewModel()
            //{
            //    Description = x.Offer.Description,
            //    ContractorName = "Name"


            //}).ToListAsync();
        }
        /*repo.AllReadonly<User>().Where(u => u.Id == x.Offer.OwnerId).Select(x => x.UserName)*/// add name to offer?
        public async Task SendOfferAsync(OfferViewModel model, int jobId, string userId)
        {
            try
            {
                // check model state?
                //check if offer already exist
                var job = await repo.GetByIdAsync<Job>(jobId);

                var offer = new Offer()
                {
                    Description = model.Description,
                    OwnerId = userId
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
            catch (Exception ms)
            {

                throw new ArgumentException(ms.Message);
            }
        }
    }
}
