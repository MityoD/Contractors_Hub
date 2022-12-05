using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Offer;

namespace ContractorsHub.Core.Contracts
{
    public interface IJobService
    {
        Task AddJobAsync(string id, JobModel model);

        Task<IEnumerable<JobViewModel>> GetAllJobsAsync();

        Task<IEnumerable<MyJobViewModel>> GetMyJobsAsync(string userId);
        
        Task<string> CompleteJob(int jobId, string userId);

        Task<JobModel> GetEditAsync(int id, string userId);

        Task PostEditAsync(int id, JobModel model);

        Task<JobViewModel> JobDetailsAsync(int id);

        Task<bool> JobExistAsync(int id);

        Task<IEnumerable<OfferServiceViewModel>> JobOffersAsync(string userId);

        Task<IEnumerable<CategoryViewModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task DeleteJobAsync(int jobId, string userId);

    }
}
