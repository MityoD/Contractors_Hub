using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository repo;

        public JobService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddJobAsync(AddJobViewModel model)
        {
            var user = await repo.GetByIdAsync<User>("ed630639-ced3-4c6a-90cb-ad0603394d22");
            var job = new Job()
            {
                Title = model.Title,
                Description = model.Description,
                Category = model.Category,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync<Job>(job);
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobViewModel>> GetAllJobsAsync()
        {
            var jobs = await repo.AllReadonly<Job>().ToListAsync();

            return jobs
                .Select(j => new JobViewModel()
                {  
                   Id = j.Id,
                   Title = j.Title,
                   Category = j.Category,
                   Description = j.Description,
                   OwnerName = j.Owner?.UserName ?? "No Name",
                   StartDate = j.StartDate
                });   
        }
    }
}
