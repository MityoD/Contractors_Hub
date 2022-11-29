using ContractorsHub.Models;
using ContractorsHub.Models.Offer;
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

        Task<IEnumerable<OfferServiceViewModel>> JobOffersAsync(string userId);

        Task<IEnumerable<CategoryViewModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

    }
}
