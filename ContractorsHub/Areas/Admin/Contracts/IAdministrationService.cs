using ContractorsHub.Models;

namespace ContractorsHub.Areas.Administration.Contracts
{
    public interface IAdministrationService
    {
        Task AddJobAsync(string id, JobModel model);

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();

        Task<JobModel> GetEditAsync(int id);

        Task PostEditAsync(int id, JobModel model);

        Task<JobViewModel> JobDetailsAsync(int id);
    }
}
