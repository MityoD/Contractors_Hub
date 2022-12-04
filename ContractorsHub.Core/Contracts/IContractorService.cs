using ContractorsHub.Core.Models.Contractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Contracts
{
    public  interface IContractorService
    {
        Task AddContractorAsync(string id, BecomeContractorViewModel model);

        Task<IEnumerable<ContractorViewModel>> AllContractorsAsync();

        Task<string> ContractorRatingAsync(string contractorId);

        public Task RateContractorAsync(string userId, string contractorId, ContractorRatingModel model);

    }
}
