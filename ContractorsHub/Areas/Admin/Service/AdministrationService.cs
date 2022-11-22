using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Areas.Administration.Service
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IRepository repo;

        public AdministrationService(IRepository _repo)
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
    }
}
