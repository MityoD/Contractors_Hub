using ContractorsHub.Core.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractorsHub.Core.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderServiceViewModel>> AllOrdersAsync();
        Task DispatchAsync(int id);
    }
}
