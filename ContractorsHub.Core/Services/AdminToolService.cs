using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
{
    public class AdminToolService : IAdminToolService
    {
        private const string DefaultImageUrl = "https://media.istockphoto.com/id/1178775481/vector/service-tools-icon-isolated-on-white-background-vector-illustration.jpg?s=612x612&w=0&k=20&c=VoGBYuv5vEW_Zbt2KIqcj2-sfEp21FGUlbZaq6QRfYY=";

        private readonly IRepository repo;

        public AdminToolService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Add tool to DB
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                ImageUrl = model.ImageUrl != null ? model.ImageUrl : DefaultImageUrl
            };


            await repo.AddAsync(tool);
            await repo.SaveChangesAsync();

        }
        /// <summary>
        /// Returns List of all ToolCategories
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if category exist by id and return bool value
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<bool> CategoryExists(int categoryId) // check if needed
        {
            return await repo.AllReadonly<ToolCategory>()
                          .AnyAsync(c => c.Id == categoryId);
        } 
        /// <summary>
        /// Check if tool exist and return bool value
        /// </summary>
        /// <param name="toolId"></param>
        /// <returns></returns>
        public async Task<bool> ToolExistAsync(int toolId) // check if needed
        {
            return await repo.AllReadonly<Tool>()
                          .AnyAsync(c => c.Id == toolId);
        }
        /// <summary>
        /// Returns ToolModel for edit for tool with Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ToolModel> GetEditAsync(int id, string userId)
        {
            if (!(await ToolExistAsync(id)))
            {
                throw new Exception("Tool don't exist!");
            }

            var tool = await repo.All<Tool>().Where(x => x.Id == id).Include(x => x.Owner).FirstOrDefaultAsync();                   

            if (tool.Owner.Id != userId)
            {
                throw new Exception("User is not owner");
            }

            if (tool.IsActive == false)
            {
                throw new Exception("This tool is not active");
            }

            var model = new ToolModel()
            {
                Title = tool.Title,
                Brand = tool.Brand,
                CategoryId = tool.ToolCategoryId,
                Description = tool.Description,
                Price = tool.Price,
                Quantity = tool.Quantity,
                ImageUrl = tool.ImageUrl

            };
            return model;
        }
        /// <summary>
        /// Post the edited tool to DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task PostEditAsync(int id, ToolModel model)
        {
            if (!(await ToolExistAsync(id)))
            {
                throw new Exception("Tool don't exist!");
            }

            var tool = await repo.All<Tool>().Where(x => x.Id == id).FirstOrDefaultAsync();

            tool.Title = model.Title;
            tool.Brand = model.Brand;
            tool.ToolCategoryId = model.CategoryId;
            tool.Description = model.Description;
            tool.ToolCategoryId = model.CategoryId;
            tool.Quantity = model.Quantity;
            tool.ImageUrl = model.ImageUrl;
            tool.Price = model.Price;

            await repo.SaveChangesAsync();
        }
        /// <summary>
        /// Change tool IsActive value to false
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RemoveToolAsync(int id, string userId)
        {
            var tool = await repo.GetByIdAsync<Tool>(id);

            if (tool == null)
            {
                throw new Exception("Invalid tool Id");
            }         

            if (tool.OwnerId != userId)
            {
                throw new Exception("Invalid user Id");
            }
           
            tool.IsActive = false;
            await repo.SaveChangesAsync();
        }
    }
}
