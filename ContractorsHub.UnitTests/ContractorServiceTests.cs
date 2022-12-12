using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Contractor;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.UnitTests
{
    public class ContractorServiceTests
    {
        private IRepository repo;
        private IContractorService service;
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
        public async Task AddContractorAsync()
        {
            service = new ContractorService(repo);

            var user = new User() { Id = "newUserId1", IsContractor = false };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new BecomeContractorViewModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "0899899889"
            };
            await service.AddContractorAsync(user.Id, model);

            var userEntity = await repo.AllReadonly<User>().Where(x => x.Id == user.Id).FirstAsync();


            Assert.That(userEntity.FirstName, Is.EqualTo("FirstName"));
            Assert.That(userEntity.LastName, Is.EqualTo("LastName"));
            Assert.That(userEntity.PhoneNumber, Is.EqualTo("0899899889"));
            Assert.That(userEntity.IsContractor, Is.EqualTo(true));
        }

        [Test]
        public async Task AddContractorAsyncThrowsException()
        {
            service = new ContractorService(repo);

            var user = new User() { Id = "newUserId1", IsContractor = false };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new BecomeContractorViewModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "0899899889"
            };


            Assert.That(async () => await service.AddContractorAsync("invalidUserId", model), Throws.Exception
             .With.Property("Message").EqualTo("User not found!"));
        }


        [Test]
        public async Task AllContractorsAsync()
        {
            service = new ContractorService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId2", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId3", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId4", IsContractor = false }
            };
        
            await repo.AddRangeAsync(newUsers);
            await repo.SaveChangesAsync();

            var result = await service.AllContractorsAsync();

            Assert.That(3, Is.EqualTo(result.Count()));
            Assert.That(result.ElementAt(0).Id == "newUserId1");
            Assert.That(result.ElementAt(1).Id == "newUserId2");
            Assert.That(result.ElementAt(2).Id == "newUserId3");                     
        }

        [Test]
        public async Task RateContractorAsync_ContractorRatingAsync()
        {
            service = new ContractorService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" },               
                new User() { Id = "newUserId2", IsContractor = false },
                new User() { Id = "newUserId3", IsContractor = false }
            };
            await repo.AddRangeAsync(newUsers);

            var jobs = new List<Job>()
            {
                new Job(){ Id = 1, IsActive = true, IsTaken = true, ContractorId = "newUserId1", OwnerId ="newUserId2", Description ="", Title = ""},
                 new Job(){ Id = 2, IsActive = true, IsTaken = true, ContractorId = "newUserId1", OwnerId ="newUserId3", Description = "", Title = ""},
            };
            await repo.AddRangeAsync(jobs);
            await repo.SaveChangesAsync();

            var model1 = new ContractorRatingModel() 
            {
                ContractorId = "newUserId1",
                Comment = "comment1",
                JobId = 1,
                Points = 5,
                UserId = "newUserId2"
            };

            await service.RateContractorAsync("newUserId2", "newUserId1", 1, model1);

            var firstRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.JobId == 1 && x.UserId == "newUserId2" && x.ContractorId == "newUserId1" && x.Comment == "comment1" && x.Points == 5).AnyAsync();

            Assert.True(firstRatingIsAdded);


            var model2 = new ContractorRatingModel()
            {
                ContractorId = "newUserId1",
                Comment = "comment2",
                JobId = 2,
                Points = 4,
                UserId = "newUserId3"
            };

            await service.RateContractorAsync("newUserId3", "newUserId1", 2, model2);

            var secondRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.JobId == 2 && x.UserId == "newUserId3" && x.ContractorId == "newUserId1" && x.Comment == "comment2" && x.Points == 4).AnyAsync();

            Assert.True(secondRatingIsAdded);


            var ratingData = await service.ContractorRatingAsync("newUserId1");
            var data = "4.50 / 5 (2 completed jobs)";

            Assert.That(ratingData, Is.EqualTo(data));
        }


        [Test]
        public async Task RateContractorAsyncThrowsException()
        {
            service = new ContractorService(repo);

            var newUsers = new List<User>()
            {
                new User() { Id = "newUserId1", IsContractor = true, FirstName = "", LastName = "", PhoneNumber = "" },
                new User() { Id = "newUserId2", IsContractor = false },
            };
            await repo.AddRangeAsync(newUsers);

            var jobs = new List<Job>()
            {
                new Job(){ Id = 1, IsActive = true, IsTaken = true, ContractorId = "newUserId1", OwnerId ="newUserId2", Description ="", Title = ""}
            };
            await repo.AddRangeAsync(jobs);
            await repo.SaveChangesAsync();

            var model1 = new ContractorRatingModel()
            {
                ContractorId = "newUserId1",
                Comment = "comment1",
                JobId = 1,
                Points = 5,
                UserId = "newUserId2"
            };


            Assert.That(async () => await service.RateContractorAsync("newUserId1", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("You can't rate yourself!"));


            Assert.That(async () => await service.RateContractorAsync("invalid", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Invalid user Id"));


            Assert.That(async () => await service.RateContractorAsync("newUserId2", "invalid", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Invalid user Id"));


            Assert.That(async () => await service.RateContractorAsync("newUserId2", "newUserId1", 2, model1),
                Throws.Exception.With.Property("Message").EqualTo("Job don't exist!"));


            await service.RateContractorAsync("newUserId2", "newUserId1", 1, model1);

            Assert.That(async () => await service.RateContractorAsync("newUserId2", "newUserId1", 1, model1),
                Throws.Exception.With.Property("Message").EqualTo("Job is already rated!"));

            //await service.RateContractorAsync("newUserId2", "newUserId1", 1, model1);

            //var firstRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.JobId == 1 && x.UserId == "newUserId2" && x.ContractorId == "newUserId1" && x.Comment == "comment1" && x.Points == 5).AnyAsync();

            //Assert.True(firstRatingIsAdded);


            //var model2 = new ContractorRatingModel()
            //{
            //    ContractorId = "newUserId1",
            //    Comment = "comment2",
            //    JobId = 2,
            //    Points = 4,
            //    UserId = "newUserId3"
            //};

            //await service.RateContractorAsync("newUserId3", "newUserId1", 2, model2);

            //var secondRatingIsAdded = await repo.AllReadonly<Rating>().Where(x => x.JobId == 2 && x.UserId == "newUserId3" && x.ContractorId == "newUserId1" && x.Comment == "comment2" && x.Points == 4).AnyAsync();

            //Assert.True(secondRatingIsAdded);


            //var ratingData = await service.ContractorRatingAsync("newUserId1");
            //var data = "4.50 / 5 (2 completed jobs)";

            //Assert.That(ratingData, Is.EqualTo(data));
        }



        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
