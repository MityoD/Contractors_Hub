using ContractorsHub.Core.Models;
using ContractorsHub.Core.Models.Tool;

namespace ContractorsHub.Core.Contracts
{
    public interface IToolService
    {
        Task AddToolAsync(ToolModel model, string id);
        Task<IEnumerable<CategoryViewModel>> AllCategories();
        Task<IEnumerable<ToolViewModel>> GetAllToolsAsync();
    }
}
