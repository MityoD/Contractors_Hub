using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace ContractorsHub.Core.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<ToolViewModel>> ViewCart(string userId);

        Task AddToCart(int toolId, string userId);

        Task RemoveFromCart(int toolId, string userId);

        IEnumerable<ToolViewModel> CheckoutCart(IFormCollection collection);

        Task<Cart> CartExists(string userId);
    }
}
