using ContractorsHub.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ContractorsHub.Services
{
    [Authorize]
    public class ContractorService : IContractorService
    {
        public Task GetAllContractorsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
