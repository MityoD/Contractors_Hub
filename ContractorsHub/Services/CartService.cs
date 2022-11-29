using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Models.Tool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Services
{
    [Authorize]
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

            bool toolCartExist = await repo.AllReadonly<ToolCart>()
                .Where(x => x.CartId == cart.Id && x.ToolId == tool.Id)
                .AnyAsync();
            if (toolCartExist)
            {
                throw new Exception("Tool already in cart");
            }
            //CHECK IF TOOL ALREADY IN CART
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

            return tools;
            
        }
    }
}
