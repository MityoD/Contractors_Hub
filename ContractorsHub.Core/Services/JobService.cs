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
        private readonly IContractorService contractorService;

        public JobService(IRepository _repo, IContractorService _contractorService)
        {
            repo = _repo;
            contractorService =_contractorService;   
        }
        /// <summary>
        /// Add Job to the DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddJobAsync(string id, JobModel model)
        {   
            var user = await repo.All<User>().Where(x=> x.Id == id).FirstOrDefaultAsync();

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
        /// <summary>
        /// Returns ModelVIew to Edit[Get] method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JobModel> GetEditAsync(int id, string userId)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Job not found");
            }

            var owner = await repo.AllReadonly<User>().Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();
      
            if (job.Owner?.Id != userId)
            {
                throw new Exception("User is not owner");
            }


            if (job.IsTaken == true)
            {
                throw new Exception("Can't edit ongoing job");
            }


            if (job.IsApproved != true)
            {
                throw new Exception("This job is not approved");
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
        /// <summary>
        /// Updates Job entity in DB with model details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task PostEditAsync(int id, JobModel model)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Job not found");
            }

            var job = await repo.All<Job>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();
        
            job.Title = model.Title;    
            job.Description = model.Description;
            job.JobCategoryId = model.CategoryId;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Returns all available Jobs
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
        /// <summary>
        /// Returns the details of Job with Id == id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<JobViewModel> JobDetailsAsync(int id)
        {
            if (!(await JobExistAsync(id)))
            {
                throw new Exception("Job not found");
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
        /// <summary>
        /// Returns bool value for Job existing in DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> JobExistAsync(int id)
        {
            var result = await repo.AllReadonly<Job>().Where(x => x.Id == id).AnyAsync();

            return result;
        }
        /// <summary>
        /// Returns the offers for all jobs of current user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<OfferServiceViewModel>> JobOffersAsync(string userId)
        {
            var jobOffers = await repo.AllReadonly<JobOffer>()
                .Where(x => x.Job.OwnerId == userId && x.Offer.IsAccepted == null
                && x.Job.IsTaken == false && x.Offer.IsActive == true && x.Job.IsActive == true).Include(j => j.Job).Include(c => c.Job.Category).Include(o => o.Offer).Include(u => u.Offer.Owner).ToListAsync();

            if (jobOffers == null)
            {
                throw new Exception("JobOffer entity error");
            }

            List<OfferServiceViewModel> offers = new List<OfferServiceViewModel>();

            foreach (var x in jobOffers)
            {
                offers.Add(new OfferServiceViewModel()
                {
                    Id = x.OfferId,
                    Description = x.Offer.Description,
                    JobDescription = x.Job.Description,
                    ContractorName = $"{x.Offer.Owner.FirstName} {x.Offer.Owner.LastName}",
                    ContractorPhoneNumber = x.Offer.Owner.PhoneNumber,
                    JobId = x.JobId,
                    JobTitle = x.Job.Title,
                    JobCategory = x.Job.Category.Name,
                    OwnerId = x.Offer.OwnerId,
                    Rating = await contractorService.ContractorRatingAsync(x.Offer.OwnerId),
                    Price = x.Offer.Price
                });
            }
            return offers;
        }
        /// <summary>
        /// Returns all job categories
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns bool for category pressent in DB
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<bool> CategoryExists(int categoryId) // check if needed
        {
            return await repo.AllReadonly<JobCategory>()
                          .AnyAsync(c => c.Id == categoryId);
        }
        /// <summary>
        /// Returns all active jobs of User with Id = userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<MyJobViewModel>> GetMyJobsAsync(string userId)
        {
            var myJobs = await repo.AllReadonly<Job>()
                .Where(j => j.OwnerId == userId && j.IsActive == true)
                .Include(j => j.Category)
                .ToListAsync();

            if (myJobs == null)
            {
                throw new Exception("Job entity error");
            }

            return myJobs
                .Select(j => new MyJobViewModel()
                {
                    Id = j.Id,
                    OwnerId = j.OwnerId,
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
        /// <summary>
        /// Marks the job as completed, and sends contractorId to
        /// ContractorController/RateContractor fot contractor rating
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> CompleteJob(int jobId, string userId)
        {  
            var job = await repo.All<Job>().Where(x => x.Id == jobId).FirstOrDefaultAsync();
            
            if (job == null)
            {
                throw new Exception("Job not found");
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
        /// <summary>
        /// Marks the jov IsActive to false
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteJobAsync(int jobId, string userId)
        {
            var job = await repo.All<Job>().Where(x => x.Id == jobId).FirstOrDefaultAsync();

            if (job == null)
            {
                throw new Exception("Job not found");
            }

            if (job.IsApproved == false)
            {
                throw new Exception("Job not reviewed");

            }

            if (job.IsTaken == true)
            {
                throw new Exception("Can't delete ongoing job");
            }
         

            if (job.OwnerId != userId)
            {
                throw new Exception("User is not owner");
            }

            var jobOffer = await repo.All<JobOffer>().Where(x => x.JobId == jobId).ToListAsync();

            if (jobOffer != null && jobOffer.Count > 0)
            {
                foreach (var jo in jobOffer)
                {
                    var offer = await repo.GetByIdAsync<Offer>(jo.OfferId);
                    offer.IsActive = false;
                }              
            }
            job.IsActive = false;
            job.Status = "Deleted";
            await repo.SaveChangesAsync();
        }
    }
}
