using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Cart;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository repo;
        public OrderService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<OrderViewModel>> AllOrdersAsync()
        {
            var result = await repo.AllReadonly<Order>().ToListAsync();
            
            return result.Select(x => new OrderViewModel()
            {
                OrderNumber = x.Id,
                OrderAdress = x.OrderAdress,
                CompletedOn = x.CompletedOn,
                ReceivedOn =x.ReceivedOn,
                IsCompleted = x.IsCompleted,
                Status = x.Status
            });
        }

        public async Task DispatchAsync(int id)
        {   //check quantity
            var order = await repo.GetByIdAsync<Order>(id);
            order.IsCompleted = true;
            order.CompletedOn = DateTime.Now;
            await repo.SaveChangesAsync();
        }
    }
}
