using ContractorsHub.Core.Models.Cart;

namespace ContractorsHub.Core.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderServiceViewModel>> AllOrdersAsync();

        Task DispatchAsync(int id);
    }
}
