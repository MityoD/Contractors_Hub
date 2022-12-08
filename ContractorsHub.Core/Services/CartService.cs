using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Cart;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json.Serialization;

namespace ContractorsHub.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repo;

        public CartService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddToCart(int toolId, string userId)
        {
            var cart =  await CartExists(userId);
            var tool = await repo.AllReadonly<Tool>().Where(x => x.Id == toolId).FirstAsync();

            if (tool == null)
            {
                throw new Exception("Tool don't exist");
            }

            bool toolCartExist = await repo.AllReadonly<ToolCart>()
                .Where(x => x.CartId == cart.Id && x.ToolId == tool.Id)
                .AnyAsync();
            if (toolCartExist)
            {
                throw new Exception("Tool already in cart");
            }

            var toolcart = new ToolCart()
            {               
                CartId = cart.Id,
                ToolId = tool.Id
            };

            await repo.AddAsync(toolcart);
            await repo.SaveChangesAsync();
        }

        public async Task<Cart> CartExists(string userId)
        {
            Cart userCart; //firstordef
            var cartExist = await repo.AllReadonly<Cart>().Where(x => x.UserId == userId).AnyAsync();

            if (!cartExist)
            {   var user = await repo.GetByIdAsync<User>(userId);
                userCart = new Cart()
                {   User = user,
                    UserId = user.Id
                };
                await repo.AddAsync(userCart);
                await repo.SaveChangesAsync();
            }
            else
            {
                userCart = await repo.AllReadonly<Cart>().Where(x => x.UserId == userId).FirstAsync();
            }
            return userCart;
        }

        public async Task RemoveFromCart(int toolId, string userId)
        {
            var cart = await repo.AllReadonly<Cart>()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                throw new Exception("Cart don't exist");
            }

            var toolCart = await repo.All<ToolCart>()
                .Where(x => x.CartId == cart.Id && x.ToolId == toolId)
                .FirstOrDefaultAsync();

            if (toolCart == null)
            {
                throw new Exception("Tool not in the cart");
            }

            repo.Delete<ToolCart>(toolCart);
            await repo.SaveChangesAsync();
        }

        public async Task CheckoutCart(IFormCollection collection, string clientId)
        {
            var count = collection["item.Id"].Count;            

            if (count == 0)
            {
                throw new Exception("Invalid data");
            }

            var sb = new StringBuilder();
            sb.AppendLine("{");
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"\"item{i+1}\" : ");
                sb.Append("{");
                sb.AppendLine($"\"itemId\" : \"{collection["item.Id"][i]}\",");
                sb.AppendLine($"\"Available\" : \"{collection["item.Quantity"][i]}\",");
                sb.AppendLine($"\"Ordered\" : \"{collection["item.OrderQuantity"][i]}\",");
                sb.AppendLine($"\"Price\" : \"{collection["item.Price"][i]}\",");
                sb.AppendLine($"\"Cost\" : \"{collection["cost"][i]}\"");
                sb.Append("},");
            }
            sb.Append($"\"TotalCost\":\"{collection["total"]}\",");
            sb.Append($"\"ToolsCount\":\"{count}\",");
            sb.Append($"\"BuyerId\":\"{clientId}\"");
            sb.Append("}");

            var itemsDetails = sb.ToString().Trim();

            //check if order exist 
            var order = new Order()
            {
                ClientId = clientId,
                ItemsDetails = itemsDetails,
                ReceivedOn = DateTime.Now,
                Status = "Preparing",
                OrderAdress = collection["address"]
            };

            await repo.AddAsync<Order>(order);

            var toolIds = collection["item.Id"];

            foreach (var id in toolIds)
            {
                await RemoveFromCart(int.Parse(id), clientId);
            };

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToolViewModel>> ViewCart(string userId)
        {
            var cart = await CartExists(userId);

            var tools = await repo.AllReadonly<ToolCart>().Where(c => c.CartId == cart.Id).Include(t => t.Tool).Include(c => c.Tool.Category).Include(t => t.Tool.Owner).Select(x => new ToolViewModel()
            {   
                Id = x.Tool.Id,
                Title = x.Tool.Title,
                Brand = x.Tool.Brand,
                Price = x.Tool.Price,
                Quantity = x.Tool.Quantity,
                Category = x.Tool.Category.Name,
                Description = x.Tool.Description,
                OwnerId = x.Tool.Owner.Id,
                OwnerName = x.Tool.Owner.UserName
            }).ToListAsync();

            if (tools == null)
            {
                throw new Exception("Tools not existing");
            }

            return tools;
            
        }

        public async Task<IEnumerable<OrderViewModel>> MyOrder(string userId)
        {
            var orders = await repo.AllReadonly<Order>()
                .Where(c => c.ClientId == userId)
                .Select(x => new OrderViewModel
                {
                    OrderNumber = x.Id,
                    OrderAdress = x.OrderAdress,
                    ReceivedOn = x.ReceivedOn,
                    Status = x.Status,
                    CompletedOn = x.CompletedOn,    
                    IsCompleted = x.IsCompleted
                })
                .ToListAsync();

            return orders;
        }
    }
}
