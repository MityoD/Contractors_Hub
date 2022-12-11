using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContractorsHub.Core.Models.Tool;

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
        public async Task Edit()
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


        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
