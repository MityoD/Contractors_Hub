using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;

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
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
            };
            await repo.AddAsync<Job>(job);
            await repo.SaveChangesAsync();
        }
    }
}
