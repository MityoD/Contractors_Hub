using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService service;

        public CartController(ICartService _service)
        {
            service = _service;
        }
 
        public async Task<IActionResult> ViewCart()
        {
            var model = await service.ViewCart(User.Id());

            return View(model);
        }
        
        public async Task<IActionResult> Add(int id)
        {
            try
            {
                await service.AddToCart(id, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Tool added to your cart";

                return RedirectToAction("All","Tool");
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = $"{ms.Message}";
                return RedirectToAction("All", "Tool");
            }

            //POP MSG
        }
    }
}
