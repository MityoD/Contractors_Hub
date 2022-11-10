using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
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
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync<Job>(job);
            await repo.SaveChangesAsync();
        }

        public async Task<JobModel> GetEditAsync(int id)
        {
            var job = await repo.GetByIdAsync<Job>(id);

                if (job == null)
                {
                throw new Exception("JOB NOT FOUND");
                }

                //string userId = ??;
                //if (userId != task.OwnerId)
                //{
                //    return Unauthorized();
                //}

                var model = new JobModel()
                {
                  Title = job.Title,
                  Description = job.Description,
                  Category = job.Category
                };

            return model;
        }

        public async Task PostEditAsync(int id, JobModel model)
        {
            var job = await repo.GetByIdAsync<Job>(id);

            if (job == null)
            {
                throw new Exception("JOB NOT FOUND");
            }

            job.Title = model.Title;    
            job.Description = model.Description;
            job.Category = model.Category;

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
                   OwnerName = j.OwnerName ?? "No name",
                   OwnerId = j.OwnerId,
                   StartDate = j.StartDate
                });

        }

        public async Task<JobViewModel> JobDetailsAsync(int id)
        {
            var job = await  repo.GetByIdAsync<Job>(id);
            var model = new JobViewModel()
            {
                Title = job.Title,
                Description = job.Description,
                Category = job.Category,
            };

            return model;
            
        }
    }
}
