using ContractorsHub.Constants;
using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
using ContractorsHub.Models.Offer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContractorsHub.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository repo;

        public JobService(IRepository _repo)
        {
            repo = _repo;
        }



        public async Task AddJobAsync(string id, JobModel model)
        {   
            var user = await repo.GetByIdAsync<User>(id);

            var job = new Job()
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                OwnerName = user.UserName,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                IsActive = true
            };
            await repo.AddAsync<Job>(job);
            await repo.SaveChangesAsync();
        }

        public async Task<JobModel> GetEditAsync(int id)
        {
            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

                if (job == null)
                {
                throw new Exception("JOB NOT FOUND");
                }

            //string userId = ??;
            //if (userId != task.OwnerId)
            //{
            //    return Unauthorized();
            //}
            var owner = await repo.GetByIdAsync<User>(job.OwnerId); //refactor
                var model = new JobModel()
                {
                  Title = job.Title,
                  Description = job.Description,
                  Category = job.Category,
                  Owner = owner,//job.Owner,//owner,
                  OwnerName = job.OwnerName                 
                  
                 
                };

            return model;
        }

        public async Task PostEditAsync(int id, JobModel model)
        {
            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

            if (job == null)
            {
                throw new Exception("JOB NOT FOUND");
            }

            job.Title = model.Title;    
            job.Description = model.Description;
            job.Category = model.Category;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobViewModel>> GetAllJobsAsync() // all open jobs
        {
            var jobs = await repo.AllReadonly<Job>().Where(j=> j.IsTaken == false && j.IsApproved == true && j.IsActive == true && j.Status == "Active").ToListAsync();

            return jobs
                .Select(j => new JobViewModel()
                {  
                   Id = j.Id,
                   Title = j.Title,
                   Category = j.Category,
                   Description = j.Description,
                   OwnerName = j.OwnerName ?? "No name",
                   OwnerId = j.OwnerId,
                   StartDate = j.StartDate
                });

        }

        public async Task<JobViewModel> JobDetailsAsync(int id)
        {
            var job = await  repo.GetByIdAsync<Job>(id);
            var model = new JobViewModel()
            {   OwnerId = job.OwnerId,
                OwnerName = job.OwnerName,
                Title = job.Title,
                Description = job.Description,
                Category = job.Category,
                Id = job.Id
            };

            return model;
            
        }

        public async Task<bool> JobExistAsync(int id)
        {
            var result = await repo.AllReadonly<Job>().Where(x => x.Id == id).FirstOrDefaultAsync();

            return result == null ? false : true;
        }

        public async Task<IEnumerable<OfferServiceViewModel>> JobOffersAsync(string userId)
        {
            var jobOffers = await repo.AllReadonly<JobOffer>()
                .Include(x => x.Job)
                .Include(o => o.Offer)
                .Where(jo => jo.Offer.IsAccepted == null)
                .Select(x => new OfferServiceViewModel()
                {
                    Id = x.OfferId,
                    Description = x.Job.Description,
                    JobId = x.JobId,
                    OwnerId = x.Offer.OwnerId,
                    Price = x.Offer.Price
                })
                .ToListAsync();

            return jobOffers;
        }
    }
}
