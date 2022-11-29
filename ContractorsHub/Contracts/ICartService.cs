using ContractorsHub.Data.Models;
using ContractorsHub.Models.Tool;

namespace ContractorsHub.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<ToolViewModel>> ViewCart(string userId);

        Task AddToCart(int toolId, string userId);

        Task<Cart> CartExists(string userId);
    }
}
