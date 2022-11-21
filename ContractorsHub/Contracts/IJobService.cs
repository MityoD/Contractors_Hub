using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Contracts
{
    public interface IJobService
    {
        Task AddJobAsync(string id, JobModel model);

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();

        Task<JobModel> GetEditAsync(int id);

        Task PostEditAsync(int id, JobModel model);

        Task<JobViewModel> JobDetailsAsync(int id);

        Task<bool> JobExistAsync(int id);

    }
}
