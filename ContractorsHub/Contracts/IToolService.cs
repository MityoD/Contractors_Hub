using ContractorsHub.Models;
using ContractorsHub.Models.Tool;

namespace ContractorsHub.Contracts
{
    public interface IToolService
    {
        Task AddToolAsync(ToolModel model, string id);
        Task<IEnumerable<CategoryViewModel>> AllCategories();
        Task<IEnumerable<ToolViewModel>> GetAllToolsAsync();
    }
}
