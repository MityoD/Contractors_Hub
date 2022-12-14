using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;

namespace ContractorsHub.UnitTests
{
    public class AdminToolServiceTests
    {
        private IRepository repo;
        private IAdminToolService service;
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
        public async Task AddToolAsync()
        {
            service = new AdminToolService(repo);

            var user = new User() { Id = "userId", IsContractor = false };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new ToolModel()
            {
                Brand = "Brand",
                Title = "Title",
                Description = "Description",
                Price = 1,
                Quantity = 1,
                CategoryId = 1,
                ImageUrl = "",
            };

            var toolBefore = await repo.AllReadonly<Tool>().Where(x => x.OwnerId == user.Id).AnyAsync();
            Assert.IsFalse(toolBefore);

            await service.AddToolAsync(model, user.Id);

            var toolAfter = await repo.AllReadonly<Tool>().Where(x => x.OwnerId == user.Id).AnyAsync();
            Assert.IsTrue(toolAfter);
        }

        [Test]
        public void AddToolAsyncThrowsException()
        {
            service = new AdminToolService(repo);

            var model = new ToolModel()
            {
                Brand = "Brand",
                Title = "Title",
                Description = "Description",
                Price = 1,
                Quantity = 1,
                CategoryId = 1,
                ImageUrl = "",
            };           


            Assert.That(async () => await service.AddToolAsync(model, "invalidId"), Throws.Exception
                .With.Property("Message").EqualTo("User entity error"));
        }

        [Test]
        public async Task AllCategories()
        {
            service = new AdminToolService(repo);

            await repo.AddRangeAsync(new List<ToolCategory>()
            {
                new ToolCategory(){ Id = 1, Name = "1"},
                new ToolCategory(){ Id = 2, Name = "2"},
                new ToolCategory(){ Id = 3, Name = "3"}
            });
            await repo.SaveChangesAsync();

            var categories = await service.AllCategories();

            Assert.That(3,Is.EqualTo(categories.Count()));
            Assert.That(categories.ElementAt(0).Name == "1");
            Assert.That(categories.ElementAt(1).Name == "2");
            Assert.That(categories.ElementAt(2).Name == "3");
        }

        [Test]
        public async Task CategoryExists()
        {
            service = new AdminToolService(repo);

            await repo.AddRangeAsync(new List<ToolCategory>()
            {
                new ToolCategory(){ Id = 1, Name = "1"},
                new ToolCategory(){ Id = 2, Name = "2"}
            });
            await repo.SaveChangesAsync();

            var category1 = await service.CategoryExists(1);
            var category2 = await service.CategoryExists(2);
            var category3 = await service.CategoryExists(3);
            var category4 = await service.CategoryExists(4);

            Assert.IsTrue(category1);
            Assert.IsTrue(category2);
            Assert.IsFalse(category3);
            Assert.IsFalse(category4);
        }

        [Test]
        public async Task ToolExistAsync()
        {
            service = new AdminToolService(repo);

            await repo.AddRangeAsync(new List<Tool>()
            {
                new Tool(){ Id = 1,Brand ="",Quantity = 1,ToolCategoryId =1, Description = "", OwnerId ="", IsActive = true, ImageUrl = "", Price = 1, Title =""},
                new Tool(){ Id = 2,Brand ="",Quantity = 1,ToolCategoryId =1, Description = "", OwnerId ="", IsActive = true, ImageUrl = "", Price = 1, Title =""}
            });
            await repo.SaveChangesAsync();

            var tool1 = await service.ToolExistAsync(1);
            var tool2 = await service.ToolExistAsync(2);
            var tool3 = await service.ToolExistAsync(3);
            var tool4 = await service.ToolExistAsync(4);

            Assert.IsTrue(tool1);
            Assert.IsTrue(tool2);
            Assert.IsFalse(tool3);
            Assert.IsFalse(tool4);
        }

        [Test]
        public async Task GetEdit()
        {
            service = new AdminToolService(repo);

            var user = new User() { Id = "userId" };
            await repo.AddAsync(user);

            await repo.AddAsync(new Tool() { Id = 1, Brand = "Brand", Quantity = 1, ToolCategoryId = 1, Description = "Description", OwnerId = user.Id,
                IsActive = true, ImageUrl = "", Price = 1, Title = "Title", Owner = user });

            await repo.SaveChangesAsync();

            var model = await service.GetEditAsync(1, "userId");

            Assert.That(model.Description, Is.EqualTo("Description"));
            Assert.That(model.Price, Is.EqualTo(1));
            Assert.That(model.Brand, Is.EqualTo("Brand"));
            Assert.That(model.CategoryId, Is.EqualTo(1));
            Assert.That(model.Quantity, Is.EqualTo(1));
            Assert.That(model.Title, Is.EqualTo("Title"));
        }

        [Test]
        public async Task GetEditThrowsException()
        {
            service = new AdminToolService(repo);   

            var user = new User() { Id = "userId" };
            await repo.AddAsync(user);

            await repo.AddAsync(new Tool()
            {
                Id = 1,
                Brand = "Brand",
                Quantity = 1,
                ToolCategoryId = 1,
                Description = "Description",
                OwnerId = user.Id,
                IsActive = false,
                ImageUrl = "",
                Price = 1,
                Title = "Title",
                Owner = user
            });

            await repo.SaveChangesAsync();

            Assert.That(async () => await service.GetEditAsync(2, "userId"), Throws.Exception
                .With.Property("Message").EqualTo("Tool don't exist!"));
            
            Assert.That(async () => await service.GetEditAsync(1, "invalidUserId"), Throws.Exception
                .With.Property("Message").EqualTo("User is not owner"));

            Assert.That(async () => await service.GetEditAsync(1, "userId"), Throws.Exception
                .With.Property("Message").EqualTo("This tool is not active"));


        }

        [Test]
        public async Task PostEdit()
        {
            service = new AdminToolService(repo);

            await repo.AddAsync(new Tool(){ Id = 1,Brand ="",Quantity = 1,ToolCategoryId =1, Description = "", OwnerId ="", IsActive = true, ImageUrl = "", Price = 1, Title =""});

            await repo.SaveChangesAsync();

            await service.PostEditAsync(1, new ToolModel()
            {
                Brand = "Brand",
                CategoryId = 2,
                Description = "Description",
                ImageUrl = "",
                Price = 200,
                Quantity = 12,
                Title = "Title"
            });

            var tool = await repo.AllReadonly<Tool>().Where(x => x.Id == 1).FirstOrDefaultAsync();

            Assert.That(tool.Description, Is.EqualTo("Description"));
            Assert.That(tool.Price, Is.EqualTo(200));
            Assert.That(tool.Brand, Is.EqualTo("Brand"));
            Assert.That(tool.ToolCategoryId, Is.EqualTo(2));
            Assert.That(tool.Quantity, Is.EqualTo(12));
            Assert.That(tool.Title, Is.EqualTo("Title"));
        }

        [Test]
        public async Task PostEditThrowsException()
        {
            service = new AdminToolService(repo);

            await repo.AddAsync(new Tool() { Id = 1, Brand = "", Quantity = 1, ToolCategoryId = 1, Description = "", OwnerId = "", IsActive = true, ImageUrl = "", Price = 1, Title = "" });

            await repo.SaveChangesAsync();
            var model = new ToolModel()
            {
                Brand = "Brand",
                CategoryId = 2,
                Description = "Description",
                ImageUrl = "",
                Price = 200,
                Quantity = 12,
                Title = "Title"
            };


            Assert.That(async () => await service.PostEditAsync(2, model), Throws.Exception
                .With.Property("Message").EqualTo("Tool don't exist!"));            
        }

        [Test]
        public async Task RemoveToolAsync()
        {
            service = new AdminToolService(repo);

            await repo.AddAsync(new Tool() { Id = 1, Brand = "", Quantity = 1, ToolCategoryId = 1, Description = "", OwnerId = "ownerId", IsActive = true, ImageUrl = "", Price = 1, Title = "" });

            await repo.SaveChangesAsync();

            var toolBefore = await repo.AllReadonly<Tool>().Where(x => x.Id == 1 && x.IsActive == true).AnyAsync();
            Assert.IsTrue(toolBefore);

            await service.RemoveToolAsync(1, "ownerId");

            var toolAfter = await repo.AllReadonly<Tool>().Where(x => x.Id == 1 && x.IsActive == false).AnyAsync();
            Assert.IsTrue(toolAfter);

        }

        [Test]
        public async Task RemoveToolAsyncThrowsException()
        {
            service = new AdminToolService(repo);

            await repo.AddAsync(new Tool() { Id = 1, Brand = "", Quantity = 1, ToolCategoryId = 1, Description = "", OwnerId = "ownerId", IsActive = true, ImageUrl = "", Price = 1, Title = "" });

            await repo.SaveChangesAsync();


            Assert.That(async () => await service.RemoveToolAsync(2, "ownerId"), Throws.Exception
                .With.Property("Message").EqualTo("Invalid tool Id"));

            Assert.That(async () => await service.RemoveToolAsync(1, "invalidId"), Throws.Exception
               .With.Property("Message").EqualTo("Invalid user Id"));
        }



        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
