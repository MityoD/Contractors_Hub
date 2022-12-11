using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Cart;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;
        public OrderService(IRepository _repo)
        {
            repo = _repo;
        }
        /// <summary>
        /// Returns all received orders to Admin area
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<OrderServiceViewModel>> AllOrdersAsync()
        {
            var result = await repo.AllReadonly<Order>().ToListAsync();
            
            return result.Select(x => new OrderServiceViewModel()
            {   Details = x.ItemsDetails,
                TotalCost = x.TotalCost,
                OrderNumber = x.Id,
                OrderAdress = x.OrderAdress,
                CompletedOn = x.CompletedOn,
                ReceivedOn =x.ReceivedOn,
                IsCompleted = x.IsCompleted,
                Status = x.Status
            });
        }
        /// <summary>
        /// Mark the order as dispatched
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DispatchAsync(int id)
        {   //check quantity
            var order = await repo.GetByIdAsync<Order>(id);
            order.IsCompleted = true;
            order.CompletedOn = DateTime.Now;
            order.Status = "Dispatched";
            await repo.SaveChangesAsync();
        }
    }
}
