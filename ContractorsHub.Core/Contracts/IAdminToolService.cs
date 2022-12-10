using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Tool;

namespace ContractorsHub.Core.Contracts
{
    public interface IAdminToolService
    {
        Task AddToolAsync(ToolModel model, string id);

        Task<IEnumerable<CategoryViewModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<bool> ToolExistAsync(int toolId);

        Task<ToolModel> GetEditAsync(int id, string userId);

        Task PostEditAsync(int id, ToolModel model);

        Task RemoveToolAsync(int id, string userId);

    }
}
