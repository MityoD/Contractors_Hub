using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Contractor;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/**/


namespace ContractorsHub.Core.Services
{
    public class ContractorService : IContractorService
    {
        private readonly IRepository repo;

        public ContractorService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddContractorAsync(string id, BecomeContractorViewModel model)
        {
            var user = await repo.GetByIdAsync<User>(id);

            if (user == null)
            {
                throw new Exception("User not found!");
            }

            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsContractor = true;
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContractorViewModel>> AllContractorsAsync()
        {
            var contractors = await repo.AllReadonly<User>()
                .Where(x => x.IsContractor == true
                && x.PhoneNumber != null 
                && x.FirstName != null 
                && x.LastName != null)
                .ToListAsync();

            if (contractors == null)
            {
                throw new Exception("Contractor entity error");
            }

            List<ContractorViewModel> result = new List<ContractorViewModel>();

            foreach (var contractor in contractors)
            {
                var data = new ContractorViewModel()
                {
                    Id = contractor.Id,
                    FirstName = contractor.FirstName ?? "First name",
                    LastName = contractor.LastName ?? "Last name",
                    PhoneNumber = contractor.PhoneNumber,
                    Rating = await ContractorRatingAsync(contractor.Id)
                };
                result.Add(data);
            }

            return result;
        }

        public async Task<string> ContractorRatingAsync(string contractorId)
        {
            double allPoints = 0;

            int ratesCount = 0;

            var rates = await repo.AllReadonly<Rating>().Where(x => x.ContractorId == contractorId).ToListAsync();

            if (rates == null)
            {
                throw new Exception("Rating entity error");
            }

            if (rates.Count > 0)
            {
                foreach (var rate in rates)
                {
                    allPoints += rate.Points;
                    ratesCount++;
                }
               
                return $"{((double)allPoints / ratesCount):F2} / 5 ({ratesCount} completed jobs)";
            }
          
            return $"Not rated";      
          
        }
        
        public async Task RateContractorAsync(string userId, string contractorId, int jobId, ContractorRatingModel model)
        {
            var user = await repo.AllReadonly<User>()
                .Where(x => x.Id == userId).AnyAsync();

            var contractor = await repo.AllReadonly<User>()
                .Where(x => x.Id == contractorId).AnyAsync();


            if (!user || !contractor)
            {
                throw new Exception("Invalid user Id");
            }

            var jobExist = await repo.AllReadonly<Job>()
                .Where(x => x.Id == jobId)
                .AnyAsync();

            if (!jobExist)
            {
                throw new Exception("Job don't exist!");
            }

            var ratingExist = await repo.AllReadonly<Rating>()
                .Where(x => x.JobId == jobId && x.UserId == userId && x.ContractorId == contractorId)
                .AnyAsync();

            if (ratingExist)
            {
                throw new Exception("Job is already rated!");
            }

            if (userId == contractorId)
            {
                throw new Exception("You can't rate yourself!");
            }

            var contractorRating = new Rating()
            {
                Comment = model.Comment,
                ContractorId = model.ContractorId,
                UserId = model.UserId,
                Points = model.Points,
                JobId = model.JobId,
            };

            await repo.AddAsync(contractorRating);
            await repo.SaveChangesAsync();
        }
    }
}
