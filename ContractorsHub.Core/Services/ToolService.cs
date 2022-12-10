using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
{
    public class ToolService : IToolService
    {        
        private readonly IRepository repo;

        public ToolService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<ToolViewModel>> GetAllToolsAsync()
        {
            var tools = await repo.AllReadonly<Tool>()
            .Where(t => t.IsActive == true)
            .Include(x => x.Owner)
            .Include(c => c.Category)
            .OrderByDescending(t => t.Id)
            .ToListAsync(); 

            if (tools == null)
            {
                throw new Exception("Tool entity error");
            }

            return tools.Select(x => new ToolViewModel()
            {   
                Id =x.Id,
                Title = x.Title,
                Brand = x.Brand,
                Description = x.Description,
                Price = x.Price,
                OwnerId = x.Owner.Id,
                OwnerName = x.Owner.UserName,
                Quantity = x.Quantity,
                Category = x.Category.Name,
                ImageUrl = x.ImageUrl
            });       
        }

        public async Task<IEnumerable<ToolServiceViewModel>> GetLastThreeTools()
        {
            return await repo.AllReadonly<Tool>()
                           .Where(x => x.IsActive)
                           .OrderByDescending(x => x.Id)
                           .Select(x => new ToolServiceViewModel()
                           {
                               Id = x.Id,
                               ImageUrl = x.ImageUrl,
                               Title = x.Title,
                               Brand = x.Brand,
                               Price = x.Price
                           })
                           .Take(3)
                           .ToListAsync();
        }
    }
}
