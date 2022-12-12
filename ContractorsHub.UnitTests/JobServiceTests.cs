using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.UnitTests
{
    public class JobServiceTests
    {
        private IRepository repo;
        private IJobService service;
        private IContractorService contractorService;
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
        public async Task AddJobAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId1", IsContractor = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new JobModel()
            {
                Title = "Test",
                Description = "",
                CategoryId = 1,
                Owner = user,
                OwnerName = ""
            };

            var jobBefore = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "Test")
                .AnyAsync();

            Assert.IsFalse(jobBefore);

            await service.AddJobAsync(user.Id, model);

            var jobAfter = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "Test")
                .AnyAsync();

            Assert.IsTrue(jobAfter);
        }

        [Test]
        public async Task AddJobAsyncThrowsException()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new JobModel()
            {
                Title = "Test",
                Description = "",
                CategoryId = 1,
                Owner = user,
                OwnerName = ""
            };

            Assert.That(async () => await service.AddJobAsync("invalidId", model),
                Throws.Exception.With.Property("Message").EqualTo("User not found"));
        }

        [Test]
        public async Task GetEditAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var job = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job);
            await repo.SaveChangesAsync();

            var jobAdded = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "TestTitle" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(jobAdded);

            var model = await service.GetEditAsync(1, user.Id);

            Assert.That(model.Description, Is.EqualTo("TestDescription"));
            Assert.That(model.Title, Is.EqualTo("TestTitle"));
        }

        [Test]
        public async Task GetEditAsyncThrowsExcepion()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var user2 = new User() { Id = "userId2", IsContractor = false };
            await repo.AddAsync(user2);

            var job = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job);
            var job2 = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 2,
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job2);
            var job3 = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 3,
                IsActive = true,
                IsApproved = false,
                IsTaken = false,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job3);
            await repo.SaveChangesAsync();

            var jobAdded = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "TestTitle" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(jobAdded);


            Assert.That(async () => await service.GetEditAsync(4, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("Job not found"));

            Assert.That(async () => await service.GetEditAsync(1, "invalidId"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Owner not found"));

            Assert.That(async () => await service.GetEditAsync(1, user2.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("User is not owner"));

            Assert.That(async () => await service.GetEditAsync(2, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("Can't edit ongoing job"));


            Assert.That(async () => await service.GetEditAsync(3, user.Id),
                Throws.Exception.With.Property("Message")
                .EqualTo("This job is not approved"));
        }


        [Test]
        public async Task PostEditAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var job = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job);
            await repo.SaveChangesAsync();

            var jobAdded = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "TestTitle" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(jobAdded);

            var model = new JobModel()
            {
                Title = "EditTitle",
                Description = "EditDescription",
                CategoryId = 2,
                Owner = user
            };

            await service.PostEditAsync(1, model);

            var editedJob = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id)
                .FirstAsync();

            Assert.That(editedJob.Title, Is.EqualTo("EditTitle"));
            Assert.That(editedJob.Description, Is.EqualTo("EditDescription"));
            Assert.That(editedJob.JobCategoryId, Is.EqualTo(2));
        }

        [Test]
        public async Task PostEditAsyncThrowsException()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var job = new Job()
            {
                Title = "TestTitle",
                Description = "TestDescription",
                JobCategoryId = 1,
                Id = 1,
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                JobStatusId = 1,
                Owner = user,
                OwnerId = user.Id,
                StartDate = DateTime.Now,
                Status = ""
            };
            await repo.AddAsync(job);
            await repo.SaveChangesAsync();

            var jobAdded = await repo.AllReadonly<Job>()
                .Where(x => x.OwnerId == user.Id && x.Title == "TestTitle" && x.Description == "TestDescription")
                .AnyAsync();

            Assert.IsTrue(jobAdded);

            var model = new JobModel()
            {
                Title = "EditTitle",
                Description = "EditDescription",
                CategoryId = 2,
                Owner = user
            };

            Assert.That(async () => await service.PostEditAsync(2, model),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job not found"));
            ;
        }

        [Test]
        public async Task GetAllJobsAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="taken", Title = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 4, Category = category, JobCategoryId =1, Description ="pending" ,Title = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Job(){Id = 5, Category = category, JobCategoryId =1, Description ="removed", Title = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();


            var jobs = await service.GetAllJobsAsync();

            Assert.That(2, Is.EqualTo(jobs.Count()));

            Assert.That(jobs.ElementAt(0).Description, Is.EqualTo("active1"));
            Assert.That(jobs.ElementAt(1).Description, Is.EqualTo("active2"));
        }

        [Test]
        public async Task JobDetailsAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="taken", Title = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 4, Category = category, JobCategoryId =1, Description ="pending" ,Title = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Job(){Id = 5, Category = category, JobCategoryId =1, Description ="removed", Title = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();

            var jobsDetails = await service.JobDetailsAsync(3);

            Assert.That(jobsDetails.Category, Is.EqualTo("category"));
            Assert.That(jobsDetails.Description, Is.EqualTo("active2"));
        }

        [Test]
        public async Task JobDetailsAsyncThrowsException()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="taken", Title = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 4, Category = category, JobCategoryId =1, Description ="pending" ,Title = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user, OwnerId = user.Id},

                new Job(){Id = 5, Category = category, JobCategoryId =1, Description ="removed", Title = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user, OwnerId = user.Id}
            });
            await repo.SaveChangesAsync();

            Assert.That(async () => await service.JobDetailsAsync(6),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job not found"));
        }
        [Test]
        public async Task JobExistAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="taken", Title = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user, OwnerId = user.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user, OwnerId = user.Id},
            });
            await repo.SaveChangesAsync();

            var firstJobExist = await service.JobExistAsync(1);
            var secondJobExist = await service.JobExistAsync(2);
            var thirdJobExist = await service.JobExistAsync(3);
            var nonExistingJob = await service.JobExistAsync(4);

            Assert.IsTrue(firstJobExist);
            Assert.IsTrue(secondJobExist);
            Assert.IsTrue(thirdJobExist);
            Assert.IsFalse(nonExistingJob);
        }

        [Test]
        public async Task JobOffersAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            var user2 = new User() { Id = "userId2", IsContractor = true, PhoneNumber ="", FirstName ="", LastName ="" };
            var user3= new User() { Id = "userId3", IsContractor = true, PhoneNumber ="", FirstName ="", LastName ="" };
            var user4 = new User() { Id = "userId4", IsContractor = false };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);
            await repo.AddAsync(user3);
            await repo.AddAsync(user4);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user4, OwnerId = user4.Id},

            });

            await repo.AddRangeAsync(new List<Offer>() 
            { 
                new Offer(){Id = 1, Description = "offer1" , IsActive = true, Owner = user2, OwnerId = user2.Id, Price = 1},

                new Offer(){Id = 2, Description = "offer2" , IsActive = true, Owner = user2, OwnerId = user2.Id, Price = 1},

                new Offer(){Id = 3, Description = "offer3" , IsActive = true, Owner = user3, OwnerId = user3.Id, Price = 1},

                 new Offer(){Id = 4, Description = "offer4" , IsActive = true, Owner = user3, OwnerId = user3.Id, Price = 1}

            });

            await repo.AddRangeAsync(new List<JobOffer>()
            {
                new JobOffer(){ JobId = 1, OfferId = 1},
                new JobOffer(){ JobId = 2, OfferId = 2},
                new JobOffer(){ JobId = 1, OfferId = 3},
                new JobOffer(){ JobId = 3, OfferId = 4}
            });

            await repo.SaveChangesAsync();

            var threeOffersUser = await service.JobOffersAsync(user1.Id);

            var oneOfferUser = await service.JobOffersAsync(user4.Id);

            Assert.That(3, Is.EqualTo(threeOffersUser.Count()));
            Assert.That(threeOffersUser.ElementAt(0).Description, Is.EqualTo("offer1"));
            Assert.That(threeOffersUser.ElementAt(1).Description, Is.EqualTo("offer2"));
            Assert.That(threeOffersUser.ElementAt(2).Description, Is.EqualTo("offer3"));

            Assert.That(1, Is.EqualTo(oneOfferUser.Count()));
            Assert.That(oneOfferUser.ElementAt(0).Description, Is.EqualTo("offer4"));
        }

        [Test]
        public async Task AllCategories()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            await repo.AddRangeAsync(new List<JobCategory>()
            {
                new JobCategory(){Id =1 ,Name = "1"},
                new JobCategory(){Id =2 ,Name = "2"},
                new JobCategory(){Id =3 ,Name = "3"},
            });

            await repo.SaveChangesAsync();

            var categories = await service.AllCategories();

            Assert.That(3, Is.EqualTo(categories.Count()));
            Assert.That(categories.ElementAt(0).Name, Is.EqualTo("1"));
            Assert.That(categories.ElementAt(1).Name, Is.EqualTo("2"));
            Assert.That(categories.ElementAt(2).Name, Is.EqualTo("3"));
        }


        [Test]
        public async Task CategoryExists()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            await repo.AddRangeAsync(new List<JobCategory>()
            {
                new JobCategory(){Id =1 ,Name = "1"}
            });

            await repo.SaveChangesAsync();

            var firstCategory = await service.CategoryExists(1);
            var secondCategory = await service.CategoryExists(2);

            Assert.IsTrue(firstCategory);
            Assert.IsFalse(secondCategory);
        }

        [Test]
        public async Task GetMyJobsAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            var user2 = new User() { Id = "userId2", IsContractor = false };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>()
            {
                new Job(){Id = 1, Category = category, JobCategoryId =1,  Description ="active1", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Job(){Id = 2, Category = category, JobCategoryId =1, Description ="taken", Title = "", IsActive = true, IsApproved = true, IsTaken = true, Status = "Taken", Owner = user1, OwnerId = user1.Id},

                new Job(){Id = 3, Category = category, JobCategoryId =1, Description ="active2", Title = "", IsActive = true, IsApproved = true, IsTaken = false, Status = "Active", Owner = user1, OwnerId = user1.Id},

                new Job(){Id = 4, Category = category, JobCategoryId =1, Description ="pending" ,Title = "", IsActive = true, IsApproved = false, IsTaken = false, Status = "Pending", Owner = user2, OwnerId = user2.Id},

                new Job(){Id = 5, Category = category, JobCategoryId =1, Description ="removed", Title = "", IsActive = false, IsApproved = true, IsTaken = false, Status = "Removed", Owner = user2, OwnerId = user2.Id}
            });
            await repo.SaveChangesAsync();

            var user1Jobs = await service.GetMyJobsAsync(user1.Id);
            var user2Jobs = await service.GetMyJobsAsync(user2.Id);

            Assert.That(3, Is.EqualTo(user1Jobs.Count()));
            Assert.That(user1Jobs.Any(h => h.Id == 4), Is.False);
            Assert.That(user1Jobs.Any(h => h.Id == 5), Is.False);
            Assert.That(1, Is.EqualTo(user2Jobs.Count()));
            Assert.That(user2Jobs.Any(h => h.Id == 5), Is.False);

        }

        [Test]
        public async Task CompleteJob()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            var user2 = new User() { Id = "userId2", IsContractor = true };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddAsync(new Job()
            {
                Id = 1,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                ContractorId = user2.Id
            });
            await repo.SaveChangesAsync();

            string contractorId = await service.CompleteJob(1, "userId1");
            var job = await repo.AllReadonly<Job>().Where(x => x.Id == 1).FirstAsync();

            Assert.IsNotNull(job);
            Assert.That(job.Status, Is.EqualTo("Completed"));
            Assert.That(job.IsActive, Is.False);
            Assert.That(contractorId, Is.EqualTo("userId2"));
        }

        [Test]
        public async Task CompleteJobThrowsException()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            var user2 = new User() { Id = "userId2", IsContractor = true };
            await repo.AddAsync(user1);
            await repo.AddAsync(user2);

            var category = new JobCategory() { Id = 1, Name = "category" };

            await repo.AddAsync(category);

            await repo.AddRangeAsync(new List<Job>(){
                new Job()
                {
                Id = 1,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                ContractorId = user2.Id
                },
                new Job()
                {
                Id = 2,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                ContractorId = user2.Id
                },
                new Job()
                {
                Id = 3,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                }

            });
            await repo.SaveChangesAsync();

            Assert.That(async () => await service.CompleteJob(4, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job not found"));


            Assert.That(async () => await service.CompleteJob(2, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job is not taken"));


            Assert.That(async () => await service.CompleteJob(3, "userId1"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job is not taken"));


            Assert.That(async () => await service.CompleteJob(1, "invalidId"),
               Throws.Exception.With.Property("Message")
               .EqualTo("Invalid user Id"));
        }

        [Test]
        public async Task DeleteJobAsync()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            await repo.AddAsync(user1);

            var user2 = new User() { Id = "userId2", IsContractor = true, FirstName ="",
                LastName = "", PhoneNumber = ""};
            await repo.AddAsync(user2);

            var category = new JobCategory() { Id = 1, Name = "category" };
            await repo.AddAsync(category);
            await repo.AddAsync(new Job()
            {
                Id = 1,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
            });

            await repo.AddAsync(new Offer() {Description ="", Id = 1, IsActive = true, Owner = user2, 
                OwnerId = user2.Id, Price = 1, IsAccepted = false});

            await repo.AddAsync(new JobOffer() { JobId = 1, OfferId = 1 });

            await repo.SaveChangesAsync();

            await service.DeleteJobAsync(1, user1.Id);

            var job = await repo.AllReadonly<Job>().Where(x => x.Id == 1).FirstOrDefaultAsync();
            var offer = await repo.AllReadonly<Offer>().Where(x => x.Id == 1).FirstOrDefaultAsync();

            Assert.IsNotNull(job);
            Assert.IsNotNull(offer);
            //Assert.That(job.Status, Is.EqualTo("Deleted"));
            Assert.That(job.IsActive, Is.False);
            Assert.That(offer.IsActive, Is.False);
        }

        [Test]
        public async Task DeleteJobAsyncThrowsException()
        {
            contractorService = new ContractorService(repo);
            service = new JobService(repo, contractorService);

            var user1 = new User() { Id = "userId1", IsContractor = false };
            await repo.AddAsync(user1);

            var category = new JobCategory() { Id = 1, Name = "category" };
            await repo.AddAsync(category);
            await repo.AddAsync(new Job()
            {
                Id = 1,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = true,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
                ContractorId = ""
            });

            await repo.AddAsync(new Job()
            {
                Id = 2,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = false,
                IsTaken = false,
                Status = "Pending",
                Owner = user1,
                OwnerId = user1.Id,
            });


            await repo.AddAsync(new Job()
            {
                Id = 3,
                Category = category,
                JobCategoryId = 1,
                Description = "active1",
                Title = "",
                IsActive = true,
                IsApproved = true,
                IsTaken = false,
                Status = "Active",
                Owner = user1,
                OwnerId = user1.Id,
            });

            await repo.SaveChangesAsync();


            Assert.That(async () => await service.DeleteJobAsync(101, user1.Id),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job not found"));


            Assert.That(async () => await service.DeleteJobAsync(2, user1.Id),
               Throws.Exception.With.Property("Message")
               .EqualTo("Job not reviewed"));

            Assert.That(async () => await service.DeleteJobAsync(1, user1.Id),
              Throws.Exception.With.Property("Message")
              .EqualTo("Can't delete ongoing job"));


            Assert.That(async () => await service.DeleteJobAsync(3, "invalidId"),
              Throws.Exception.With.Property("Message")
              .EqualTo("User is not owner"));
        }


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
