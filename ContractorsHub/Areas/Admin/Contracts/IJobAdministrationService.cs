using ContractorsHub.Areas.Administration.Models;
using ContractorsHub.Core.Models;

namespace ContractorsHub.Areas.Administration.Contracts
{
    public interface IJobAdministrationService
    {

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();

        Task<JobViewModel> JobDetailsAsync(int id);

        Task ApproveJobAsync(int id);

        Task DeclineJobAsync(int id);

        Task<IEnumerable<JobViewAdminModel>> ReviewPendingJobs();

        Task<IEnumerable<JobViewAdminModel>> ReviewDeclinedJobs();

        Task<IEnumerable<JobViewAdminModel>> ReviewActiveJobs();



        //
        Task<JobModel> GetEditAsync(int id);

        Task PostEditAsync(int id, JobModel model);

    }
}
