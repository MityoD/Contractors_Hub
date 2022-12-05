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

        public async Task AddToolAsync(ToolModel model, string id)
        {
            var user = await repo.GetByIdAsync<User>(id);

            if (user == null)
            {
                throw new Exception("User entity error");
            }

            var tool = new Tool()
            {
                Title = model.Title,
                Brand = model.Brand,              
                ToolCategoryId = model.CategoryId,
                Description = model.Description,
                Owner = user,
                OwnerId = user.Id,
                Price = model.Price,
                Quantity = model.Quantity,
                IsActive = true,
            };


            await repo.AddAsync(tool);
            await repo.SaveChangesAsync();
    
        }

        public async Task<IEnumerable<CategoryViewModel>> AllCategories()
        {
            return await repo.AllReadonly<ToolCategory>()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ToolViewModel>> GetAllToolsAsync()
        {
            var tools = await repo.AllReadonly<Tool>()
            .Where(t => t.IsActive == true).Include(x => x.Owner).Include(c => c.Category).ToListAsync(); 

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
                Category = x.Category.Name
            });       
        }
    }
}
