using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Areas.Administration.Models;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Areas.Administration.Service
{
    public class JobAdministrationService : IJobAdministrationService
    {
        private readonly IRepository repo;

        public JobAdministrationService(IRepository _repo)
        {
           repo = _repo;   
        }

        public Task AddJobAsync(string id, JobModel model)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveJobAsync(int id)
        {
            var job = await repo.GetByIdAsync<Job>(id);
            job.IsApproved = true;
            job.IsActive = true;
            job.Status = "Active";
            job.JobStatusId = 2;
            await repo.SaveChangesAsync();

        }

        public async Task DeclineJobAsync(int id)
        {
            var job = await repo.GetByIdAsync<Job>(id);
            job.IsApproved = false;
            job.IsActive = false;
            job.Status = "Declined";
            job.JobStatusId = 3;
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobViewModel>> GetAllJobsAsync()
        {
            return await repo.All<Job>().Select(x=> new JobViewModel()
            {
                Id = x.Id,
                Category = x.Category,
                Description = x.Description,
                OwnerId = x.OwnerId,
                OwnerName = x.OwnerName, // change nullable!!!
                StartDate = x.StartDate,
                Title = x.Title 
                
            }).ToListAsync();
        }

        public Task<JobModel> GetEditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<JobViewModel> JobDetailsAsync(int id)
        {
            var job = await repo.GetByIdAsync<Job>(id);
            var model = new JobViewModel()
            {
                OwnerId = job.OwnerId,
                OwnerName = job.OwnerName,
                Title = job.Title,
                Description = job.Description,
                Category = job.Category,
                Id = job.Id,
                StartDate = job.StartDate,
               
                
            };

            return model;
        }

        public Task PostEditAsync(int id, JobModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobViewAdminModel>> ReviewPendingJobs()
        { 
            var result = await repo.All<Job>().Where(j => j.IsApproved == false && j.JobStatusId == 1).Select(j => new JobViewAdminModel()
            {
                Category = j.Category,
                ContractorId = j.ContractorId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Title = j.Title,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }
        public async Task<IEnumerable<JobViewAdminModel>> ReviewDeclinedJobs()
        { 
            var result = await repo.All<Job>().Where(j => j.IsApproved == false && j.JobStatusId == 3 && j.IsActive == false).Select(j => new JobViewAdminModel()
            {
                Category = j.Category,
                ContractorId = j.ContractorId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Title = j.Title,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<JobViewAdminModel>> ReviewActiveJobs()
        {
            var result = await repo.All<Job>().Where(j => j.IsApproved == true && j.JobStatusId == 2 && j.IsActive == true).Select(j => new JobViewAdminModel()
            {
                Category = j.Category,
                ContractorId = j.ContractorId,
                Description = j.Description,
                Id = j.Id,
                OwnerId = j.OwnerId,
                OwnerName = j.OwnerName,
                Title = j.Title,
                StartDate = j.StartDate
            }).ToListAsync();

            return result;
        }
    }
}
