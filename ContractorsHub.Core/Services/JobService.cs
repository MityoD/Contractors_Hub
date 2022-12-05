using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Offer;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
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

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var job = new Job()
            {
                Title = model.Title,
                Description = model.Description,
                JobCategoryId = model.CategoryId,
                OwnerName = user.UserName,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                IsActive = true
            };
            await repo.AddAsync<Job>(job);
            await repo.SaveChangesAsync();
        }

        public async Task<JobModel> GetEditAsync(int id, string userId)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Non existing job!");
            }

            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();

            if (job == null)
            {
                throw new Exception("Job not found");
            }

            if (job.Owner?.Id != userId)
            {
                throw new Exception("User is not owner");
            }

            var owner = await repo.GetByIdAsync<User>(job.OwnerId); //refactor
            
            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            var model = new JobModel()
            {
              Title = job.Title,
              Description = job.Description,
              CategoryId = job.JobCategoryId,
              Owner = owner,
              OwnerName = job.OwnerName                      
            };
            return model;
        }

        public async Task PostEditAsync(int id, JobModel model)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Non existing job!");
            }

            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();
        
            job.Title = model.Title;    
            job.Description = model.Description;
            job.JobCategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobViewModel>> GetAllJobsAsync()
        {
            var jobs = await repo.AllReadonly<Job>().Where(j=> j.IsTaken == false && j.IsApproved == true && j.IsActive == true && j.Status == "Active").Include(j => j.Category).ToListAsync();

            if (jobs == null)
            {
                throw new Exception("Job entity error");
            }

            return jobs
                .Select(j => new JobViewModel()
                {  
                   Id = j.Id,
                   Title = j.Title,
                   Category = j.Category.Name,
                   Description = j.Description,
                   OwnerName = j.OwnerName,
                   OwnerId = j.OwnerId,
                   StartDate = j.StartDate
                });
        }

        public async Task<JobViewModel> JobDetailsAsync(int id)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Non existing job!");
            }

            var job = await  repo.AllReadonly<Job>()
                .Where(j => j.Id == id)
                .Include(c => c.Category)
                .FirstAsync();


            var model = new JobViewModel()
            {   OwnerId = job.OwnerId,
                OwnerName = job.OwnerName,
                Title = job.Title,
                Description = job.Description,
                Category = job.Category.Name,
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
                .Where(x => x.Job.OwnerId == userId && x.Offer.IsAccepted == null && x.Job.IsTaken == false)
                .Select(x => new OfferServiceViewModel()
                {
                    Id = x.OfferId,
                    Description = x.Job.Description,
                    JobId = x.JobId,
                    OwnerId = x.Offer.OwnerId,
                    Price = x.Offer.Price
                })
                .ToListAsync();

            if (jobOffers == null)
            {
                throw new Exception("JobOffer entity error");
            }

            return jobOffers;
        }

        public async Task<IEnumerable<CategoryViewModel>> AllCategories()
        {
            return await repo.AllReadonly<JobCategory>()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId) // check if needed
        {
            return await repo.AllReadonly<JobCategory>()
                          .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<IEnumerable<MyJobViewModel>> GetMyJobsAsync(string userId)
        {
            var myJobs = await repo.AllReadonly<Job>().Where(j => j.OwnerId == userId).Include(j => j.Category).ToListAsync();
            // include owner?

            if (myJobs == null)
            {
                throw new Exception("Job entity error");
            }

            return myJobs
                .Select(j => new MyJobViewModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Category = j.Category.Name,
                    Description = j.Description,
                    IsTaken = j.IsTaken,
                    IsActive = j.IsActive,
                    IsApproved = j.IsApproved,
                    ContractorId = j.ContractorId,
                    EndDate = j.EndDate,
                    StartDate = j.StartDate,
                    Status = j.Status
                });
        }

        public async Task<string> CompleteJob(int jobId, string userId)
        {
            var job = await repo.GetByIdAsync<Job>(jobId);
            
            if (job == null)
            {
                throw new Exception("Invalid job Id");
            }

            if (job.IsTaken == false || job.ContractorId == null)
            {
                throw new Exception("Job is not taken");
            }
        
            if (job.OwnerId != userId)
            {
                throw new Exception("Invalid user Id");
            }

            var contractorId = job.ContractorId; 

            job.IsActive = false;
            job.EndDate = DateTime.Now;
            job.Status = "Completed";
            await repo.SaveChangesAsync();

            return contractorId;
        }

        public Task DeleteJobAsync(int jobId)
        {
            throw new NotImplementedException();
        }
    }
}
