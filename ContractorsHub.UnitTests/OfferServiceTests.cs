using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Offer;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.UnitTests
{
    public class OfferServiceTests
    {
        private IRepository repo;
        private IOfferService service;
        private ApplicationDbContext context;


        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("Contractors_Hub_DB")
               .Options;

            context = new ApplicationDbContext(contextOptions);
            repo = new Repository(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        [Test]
        public async Task AcceptOfferAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true };
            var user2 = new User() { Id = "newUserId2", IsContractor = false };


            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
            };
            await repo.AddRangeAsync(offers);


            var jobs = new List<Job>()
            {
              new Job(){Id = 1, Title ="", Description ="", Owner = user2, OwnerId = user2.Id, IsActive = true, JobCategoryId = 1, IsApproved = true, IsTaken = false, Status = "", StartDate = DateTime.Now, JobStatusId = 1},

               new Job(){Id = 2, Title ="", Description ="", Owner = user2, OwnerId = user2.Id, IsActive = true, JobCategoryId = 1, IsApproved = true, IsTaken = false, Status = "", StartDate = DateTime.Now, JobStatusId = 1},
            };
            await repo.AddRangeAsync(jobs);

            var jobOffers = new List<JobOffer>()
            {
                new JobOffer(){JobId = 1, OfferId = 1}
            };
            await repo.AddRangeAsync(jobOffers);
            await repo.SaveChangesAsync();

            await service.AcceptOfferAsync(1);

            var dbOffer = await repo.GetByIdAsync<Offer>(1);
            var dbJob = await repo.GetByIdAsync<Job>(1);

            var _dbOffer = await repo.GetByIdAsync<Offer>(2);
            var _dbJob = await repo.GetByIdAsync<Job>(2);

            Assert.That(dbOffer.IsAccepted == true);
            Assert.That(dbJob.IsTaken == true);
            Assert.That(dbJob.ContractorId == "newUserId1");

            Assert.That(_dbOffer.IsAccepted == null);
            Assert.That(_dbJob.IsTaken == false);
            Assert.That(_dbJob.ContractorId == null);
        }

        [Test]
        public async Task DeclineOfferAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true };
            var user2 = new User() { Id = "newUserId2", IsContractor = false };


            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
            };
            await repo.AddRangeAsync(offers);


            var jobs = new List<Job>()
            {
              new Job(){Id = 1, Title ="", Description ="", Owner = user2, OwnerId = user2.Id, IsActive = true, JobCategoryId = 1, IsApproved = true, IsTaken = false, Status = "", StartDate = DateTime.Now, JobStatusId = 1},

               new Job(){Id = 2, Title ="", Description ="", Owner = user2, OwnerId = user2.Id, IsActive = true, JobCategoryId = 1, IsApproved = true, IsTaken = false, Status = "", StartDate = DateTime.Now, JobStatusId = 1},
            };
            await repo.AddRangeAsync(jobs);

            var jobOffers = new List<JobOffer>()
            {
                new JobOffer(){JobId = 1, OfferId = 1}
            };
            await repo.AddRangeAsync(jobOffers);
            await repo.SaveChangesAsync();

            await service.DeclineOfferAsync(1);

            var dbOffer = await repo.GetByIdAsync<Offer>(1);
            var dbJob = await repo.GetByIdAsync<Job>(1);

            var _dbOffer = await repo.GetByIdAsync<Offer>(2);
            var _dbJob = await repo.GetByIdAsync<Job>(2);

            Assert.That(dbOffer.IsAccepted == false);
            Assert.That(dbJob.IsTaken == false);
            Assert.That(dbJob.ContractorId == null);

            Assert.That(_dbOffer.IsAccepted == null);
            Assert.That(_dbJob.IsTaken == false);
            Assert.That(_dbJob.ContractorId == null);
        }

        [Test]
        public async Task GetOfferAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true };


            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="First", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="Second", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
            };
            await repo.AddRangeAsync(offers);
            await repo.SaveChangesAsync();

            var firstOffer = await service.GetOfferAsync(1);
            var secondOffer = await service.GetOfferAsync(2);

            Assert.That(firstOffer.Description == "First");
            Assert.That(secondOffer.Description == "Second");
        }

        [Test]
        public async Task OfferExists()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true };


            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="First", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="Second", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
            };
            await repo.AddRangeAsync(offers);
            await repo.SaveChangesAsync();

            var firstOffer = await service.OfferExists(1);
            var secondOffer = await service.OfferExists(2);
            var thirdOffer = await service.OfferExists(3);

            Assert.That(firstOffer == true);
            Assert.That(secondOffer == true);
            Assert.That(thirdOffer == false);
        }

        [Test]
        public async Task OfferConditionAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true, FirstName ="", LastName = "", PhoneNumber = "" };
            var user2 = new User() { Id = "newUserId2", IsContractor = true , FirstName = "", LastName = "", PhoneNumber = "" };
            var user3 = new User() { Id = "newUserId3", IsContractor = false };


            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="First", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="Second", Owner = user1, OwnerId = user1.Id, IsAccepted = null, IsActive = true, Price = 1},
                new Offer(){Id = 3, Description ="Third", Owner = user2, OwnerId = user2.Id, IsAccepted = null, IsActive = true, Price = 1}
            };
            await repo.AddRangeAsync(offers);

            var jobCategory = new JobCategory() { Id = 1, Name = "Category" };
            await repo.AddAsync(jobCategory);

            var jobStatus = new JobStatus() { Id = 1, Name = "Status" };
            await repo.AddAsync(jobStatus);


            var jobs = new List<Job>()
            {
              new Job(){Id = 1, Title ="", Description ="", Owner = user3, OwnerId = user3.Id, IsActive = true, JobCategoryId = jobCategory.Id, Category = jobCategory, IsApproved = true, IsTaken = false, Status = "Pending", StartDate = DateTime.Now, JobStatusId = 1, JobStatus = jobStatus}
            };
            await repo.AddRangeAsync(jobs);

            var jobOffers = new List<JobOffer>()
            {
                new JobOffer(){JobId = 1, OfferId = 1},
                new JobOffer(){JobId = 1, OfferId = 2},
                new JobOffer(){JobId = 1, OfferId = 3}
            };
            await repo.AddRangeAsync(jobOffers);

            await repo.SaveChangesAsync();

            var offerConditions = await service.OffersConditionAsync(user1.Id);

            Assert.That(2, Is.EqualTo(offerConditions.Count()));
            Assert.That(offerConditions.ElementAt(0).Description == "First");
            Assert.That(offerConditions.ElementAt(1).Description == "Second");
        }

        [Test]
        public async Task RemoveOfferAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" };         

            var offers = new List<Offer>()
            {
              new Offer(){Id = 1, Description ="First", Owner = user1, OwnerId = user1.Id, IsAccepted = false, IsActive = true, Price = 1},
              new Offer(){Id = 2, Description ="Second", Owner = user1, OwnerId = user1.Id, IsAccepted = false, IsActive = true, Price = 1},
                new Offer(){Id = 3, Description ="Third", Owner = user1, OwnerId = user1.Id, IsAccepted = false, IsActive = true, Price = 1}
            };
            await repo.AddRangeAsync(offers);            
            await repo.SaveChangesAsync();

            var userActiveOffers = await repo.AllReadonly<Offer>().Where(x => x.OwnerId == user1.Id && x.IsActive == true).ToListAsync();

            Assert.That(3, Is.EqualTo(userActiveOffers.Count()));

            await service.RemoveOfferAsync(user1.Id, 2);

            userActiveOffers = await repo.AllReadonly<Offer>().Where(x => x.OwnerId == user1.Id && x.IsActive == true).ToListAsync();
            
            Assert.That(2, Is.EqualTo(userActiveOffers.Count()));

            await service.RemoveOfferAsync(user1.Id, 1);

            userActiveOffers = await repo.AllReadonly<Offer>().Where(x => x.OwnerId == user1.Id && x.IsActive == true).ToListAsync();

            Assert.That(1, Is.EqualTo(userActiveOffers.Count()));

            await service.RemoveOfferAsync(user1.Id, 3);

            userActiveOffers = await repo.AllReadonly<Offer>().Where(x => x.OwnerId == user1.Id && x.IsActive == true).ToListAsync();

            Assert.That(0, Is.EqualTo(userActiveOffers.Count()));


        }

        [Test]
        public async Task SendOfferAsync()
        {
            service = new OfferService(repo);

            var user1 = new User() { Id = "newUserId1", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" };           
            var user3 = new User() { Id = "newUserId3", IsContractor = false };
                       

            var jobCategory = new JobCategory() { Id = 1, Name = "Category" };
            await repo.AddAsync(jobCategory);

            var jobStatus = new JobStatus() { Id = 1, Name = "Status" };
            await repo.AddAsync(jobStatus);


            var jobs = new List<Job>()
            {
              new Job(){Id = 1, Title ="", Description ="", Owner = user3, OwnerId = user3.Id, IsActive = true, JobCategoryId = jobCategory.Id, Category = jobCategory, IsApproved = true, IsTaken = false, Status = "Pending", StartDate = DateTime.Now, JobStatusId = 1, JobStatus = jobStatus}
            };
            await repo.AddRangeAsync(jobs);
            await repo.SaveChangesAsync();

            var model = new OfferViewModel()
            {
                OwnerId = user1.Id,
                Description = "",
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                JobId = 1,
                Price = 200
            };

            var jobOfferBefore = await repo.AllReadonly<JobOffer>().Where(x => x.JobId == 1 && x.Offer.OwnerId == user1.Id).AnyAsync();

            await service.SendOfferAsync(model, 1, user1.Id);

            var jobOfferAfter = await repo.AllReadonly<JobOffer>().Where(x => x.JobId == 1 && x.Offer.OwnerId == user1.Id).AnyAsync();

            Assert.IsTrue(jobOfferAfter);
            Assert.IsFalse(jobOfferBefore);
        }


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
