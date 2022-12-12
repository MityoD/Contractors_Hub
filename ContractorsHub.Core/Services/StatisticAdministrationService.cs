using ContractorsHub.Core.Models.Admin;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;

namespace ContractorsHub.Core.Services
{
    public class StatisticAdministrationService
    {
        private readonly IRepository repo;
        public StatisticAdministrationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<StatisticViewModel> StatisticAsync()
        {

            /*Pending
            Approved
            Declined
            Deleted
            Completed*/

            int allJobs = repo.AllReadonly<Job>().Count();
            int activeJobs = repo.AllReadonly<Job>().Where(x => x.Status == "Active").Count();
            int completedJobs = repo.AllReadonly<Job>().Where(x => x.Status == "Completed").Count();
            int pendingJobs = repo.AllReadonly<Job>().Where(x => x.Status == "Pending").Count();
            int declinedJobs = repo.AllReadonly<Job>().Where(x => x.Status == "Declined").Count();

            return new StatisticViewModel()
            {
                ActiveJobs = activeJobs,
                AllJobs = allJobs,
                PendingJobs = pendingJobs,
                DeclinedJobs = declinedJobs,
                CompletedJobs = completedJobs
            };
        }
    }
}
