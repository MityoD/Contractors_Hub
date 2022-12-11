using ContractorsHub.Core.Models.Tool;

namespace ContractorsHub.Core.Contracts
{
    public interface IToolService
    {
        Task<IEnumerable<ToolViewModel>> GetAllToolsAsync();

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<AllToolsQueryModel> AllToolsAsync(
            string? category = null,
            string? searchTerm = null,
            ToolSorting sorting = ToolSorting.Newest,
            int currentPage = 1,
            int toolsPerPage = 1);

        Task<IEnumerable<ToolServiceViewModel>> GetLastThreeTools();
    }
}
