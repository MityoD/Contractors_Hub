using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Contracts
{
    public interface IJobService
    {
        Task AddJobAsync(AddJobViewModel model);

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();
    }
}
