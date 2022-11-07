using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Contracts
{
    public interface IJobService
    {
        public Task AddJobAsync(AddJobViewModel model);
    }
}
