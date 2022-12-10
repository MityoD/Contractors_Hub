using ContractorsHub.Core.Models.Tool;

namespace ContractorsHub.Core.Contracts
{
    public interface IToolService
    {
        Task<IEnumerable<ToolViewModel>> GetAllToolsAsync();

        Task<IEnumerable<ToolServiceViewModel>> GetLastThreeTools();
    }
}
