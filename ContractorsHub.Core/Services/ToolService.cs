using ContractorsHub.Core.Contracts;
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

        public async Task<AllToolsQueryModel> AllToolsAsync(string? category = null, string? searchTerm = null, ToolSorting sorting = ToolSorting.Newest, int currentPage = 1, int toolsPerPage = 1)
        {
            var result = new AllToolsQueryModel();
            var tools = repo.AllReadonly<Tool>()
                .Where(t => t.IsActive);

            if (string.IsNullOrEmpty(category) == false)
            {
                tools = tools
                    .Where(t => t.Category.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                tools = tools
                    .Where(t => EF.Functions.Like(t.Title.ToLower(), searchTerm) ||
                        EF.Functions.Like(t.Brand.ToLower(), searchTerm) ||
                        EF.Functions.Like(t.Description.ToLower(), searchTerm));
            }           

            tools = sorting switch
            {
                //ToolSorting.NotRentedFirst => houses
                //    .OrderBy(h => h.RenterId),


                ToolSorting.Price => tools
                    .OrderBy(t => t.Price),
                _ => tools.OrderByDescending(t => t.Id)
            };

            result.Tools = await tools
                .Skip((currentPage - 1) * toolsPerPage)
                .Take(toolsPerPage)
                .Select(t => new ToolViewModel()
                {  
                    Id = t.Id,
                    Title = t.Title,
                    Brand = t.Brand,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    ImageUrl = t.ImageUrl,
                    Description = t.Description,
                    Category = t.Category.Name         
                })
                .ToListAsync();

            result.TotalToolsCount = await tools.CountAsync();

            return result;
        }
        /// <summary>
        /// Returns the names of all ToolCategory from DB
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await repo.AllReadonly<ToolCategory>()
                 .Select(c => c.Name)
                 .Distinct()
                 .ToListAsync();
        }
        /// <summary>
        /// Returns all tools from DB for administration area
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                Id = x.Id,
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
        /// <summary>
        ///  Returns last three added tools from DB
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<ToolServiceViewModel>> GetLastThreeTools()
        {
            var result = await repo.AllReadonly<Tool>()
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

            if (result == null)
            {
                throw new Exception("DB Error");
            }
            
            return result;
        }
    }
}
