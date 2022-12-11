using ContractorsHub.Core.Contracts;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ContractorsHub.Core.Services;
using ContractorsHub.Infrastructure.Data.Models;

namespace ContractorsHub.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}


//private IRepository repo;
//private IOrderService service;
//private ApplicationDbContext context;


//[SetUp]
//public void Setup()
//{
//    var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//       .UseInMemoryDatabase("Contractors_Hub_DB")
//       .Options;

//    context = new ApplicationDbContext(contextOptions);
//    repo = new Repository(context);

//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();
//}
////[Test]
////public void TestLastThreeToolsThrowsError()
////{
////    toolService = new ToolService(repo);

////    Assert.That(
////        async () => await toolService.GetLastThreeTools(),
////        Throws.Exception.TypeOf<Exception>());
////}
//[Test]
//public async Task TestAllCategoriesNamesReturnsValidData()
//{
//    service = new ToolService(repo);

//    await repo.AddRangeAsync(new List<ToolCategory>()
//            {
//                new ToolCategory(){ Id = 101 , Name = "First" },
//                new ToolCategory(){ Id = 102 , Name = "Second" },
//                new ToolCategory(){ Id = 103 , Name = "Second" }
//            });

//    await repo.SaveChangesAsync();

//    var categoryNames = await service.AllCategoriesNames();

//    Assert.That(2, Is.EqualTo(categoryNames.Count()));
//    Assert.AreEqual(categoryNames, new List<string>() { "First", "Second" });
//}

//[TearDown]
//public void TearDown()
//{
//    context.Dispose();
//}