using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

//Add thrown exceptions
namespace ContractorsHub.UnitTests
{
    public class ToolServiceTests
    {
        private IRepository repo;
        private IToolService service;
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
        public async Task TestAllToolsAsyncReturnsValidData()
        {
            Assert.Pass();

            //Implement logic
        }

        [Test]
        public async Task TestAllCategoriesNamesReturnsValidData()
        {
            service = new ToolService(repo);

            await repo.AddRangeAsync(new List<ToolCategory>()
            {
                new ToolCategory(){ Id = 101 , Name = "First" },
                new ToolCategory(){ Id = 102 , Name = "Second" },
                new ToolCategory(){ Id = 103 , Name = "Second" }
            });

            await repo.SaveChangesAsync();

            var categoryNames = await service.AllCategoriesNames();

            Assert.That(2, Is.EqualTo(categoryNames.Count()));
            Assert.AreEqual(categoryNames, new List<string>() { "First", "Second" });
        }

        [Test]
        public async Task TestLastThreeToolsReturnsValidData()
        {
            service = new ToolService(repo);

            await repo.AddRangeAsync(new List<Tool>() 
            {
                new Tool(){Id = 101,ImageUrl ="", Price = 1, OwnerId = "",Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 102,ImageUrl ="", Price = 1, OwnerId = "",Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 105,ImageUrl ="", Price = 1, OwnerId = "",Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 107,ImageUrl ="", Price = 1, OwnerId = "",Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""}
            });

            await repo.SaveChangesAsync();

            var tools = await service.GetLastThreeTools();

            Assert.That(3, Is.EqualTo(tools.Count()));
            Assert.That(tools.Any(h => h.Id == 101), Is.False);
        }

        [Test]
        public async Task TestGetAllToolsAsyncReturnsValidData()
        {
            var user = new User() { Id = "newUserId", IsContractor = false };
            var category = new ToolCategory() { Id = 1001, Name = "Category" };

            service = new ToolService(repo);

            var toolList = new List<Tool>()
            {
                new Tool(){Id = 107,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 106,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 105,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 104,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 103,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 102,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""},
                new Tool(){Id = 101,ImageUrl ="", Price = 1, OwnerId = "",Owner = user, Category = category, Description = "", ToolCategoryId = 1,Quantity = 1, Brand = "", Title = ""}
            };

            await repo.AddRangeAsync(toolList);           

            await repo.SaveChangesAsync();

            var tools = await service.GetAllToolsAsync();

            Assert.That(7, Is.EqualTo(tools.Count()));


        }

        //[Test]
        //public void TestLastThreeToolsThrowsError()
        //{
        //    toolService = new ToolService(repo);

        //    Assert.That(
        //        async () => await toolService.GetLastThreeTools(),
        //        Throws.Exception.TypeOf<Exception>());
        //}

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
