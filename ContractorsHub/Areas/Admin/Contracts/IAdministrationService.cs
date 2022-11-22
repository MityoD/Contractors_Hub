using ContractorsHub.Models;

namespace ContractorsHub.Areas.Administration.Contracts
{
    public interface IAdministrationService
    {

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();

        Task<JobViewModel> JobDetailsAsync(int id);

        Task ApproveJobAsync(int id);




        Task<JobModel> GetEditAsync(int id);

        Task PostEditAsync(int id, JobModel model);

    }
}
