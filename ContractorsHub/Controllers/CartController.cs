﻿using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService service;
        private readonly ILogger<CartController> logger;

        public CartController(ICartService _service, ILogger<CartController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var model = await service.ViewCart(User.Id());
    
                return View(model);

            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
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
                //logger log exception
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                logger.LogError(ms.Message, ms);
                return RedirectToAction("All", "Tool");
            }
        }

        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await service.RemoveFromCart(id, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Tool removed from your cart";

                return RedirectToAction(nameof(ViewCart));
            }
            catch (Exception ms)
            {
                //logger log exception
                TempData[MessageConstant.ErrorMessage] = "Something went wrong";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(ViewCart));
            }
        }

        public async Task<IActionResult> Checkout(IFormCollection collection)
        {

            try
            {
                await service.CheckoutCart(collection, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Order is sent!";

            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("All","Tool");
        }

        public async Task<IActionResult> MyOrder()
        {
            try
            {
                var model = await service.MyOrder(User.Id());
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
