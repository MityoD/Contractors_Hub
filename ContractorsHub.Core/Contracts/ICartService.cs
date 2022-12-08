using ContractorsHub.Core.Models.Cart;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

namespace ContractorsHub.Core.Contracts
{
    public interface ICartService
    {
        Task<IEnumerable<ToolViewModel>> ViewCart(string userId);

        Task<IEnumerable<OrderViewModel>> MyOrder(string userId);

        Task AddToCart(int toolId, string userId);

        Task RemoveFromCart(int toolId, string userId);

        Task CheckoutCart(IFormCollection collection, string clientId);

        Task<Cart> CartExists(string userId);
    }
}
