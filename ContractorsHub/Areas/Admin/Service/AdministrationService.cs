using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Models;

namespace ContractorsHub.Areas.Administration.Service
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IRepository repo;

        public AdministrationService(IRepository _repo)
        {
           repo = _repo;   
        }

        public Task AddJobAsync(string id, JobModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobViewModel>> GetAllJobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<JobModel> GetEditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobViewModel> JobDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PostEditAsync(int id, JobModel model)
        {
            throw new NotImplementedException();
        }
    }
}
