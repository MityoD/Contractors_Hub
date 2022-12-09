﻿using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class OrderController : Controller
    {
        private readonly IOrderService service;

        public OrderController(IOrderService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var model = await service.AllOrdersAsync();
            return View(model);
        }
        public async Task<IActionResult> Dispatch(int id)
        {
            await service.DispatchAsync(id);
            return RedirectToAction(nameof(All));
        }



    }
}
