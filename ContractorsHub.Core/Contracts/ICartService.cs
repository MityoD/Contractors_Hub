using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.Core.Models.Tool;

namespace ContractorsHub.Core.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<ToolViewModel>> ViewCart(string userId);

        Task AddToCart(int toolId, string userId);

        Task RemoveFromCart(int toolId, string userId);

        Task<Cart> CartExists(string userId);
    }
}
