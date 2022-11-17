using ContractorsHub.Constants;
using ContractorsHub.Contracts;
using ContractorsHub.Data.Common;
using ContractorsHub.Data.Models;
using ContractorsHub.Extensions;
using ContractorsHub.Models.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private readonly IOfferService service;
        public OfferController(IOfferService _service)
        {
            service = _service;
        }
            
        [HttpGet]
        [Authorize(Roles = RoleConstants.Contractor)]
        public IActionResult Send()
        {
            var model = new OfferViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Contractor)]
        public async Task<IActionResult> Send(int id, OfferViewModel model)
        {
            await service.SendOfferAsync(model, id, User.Id());

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyOffers()
        {
           var offers = await service.MyOffersAsync(User.Id());

            return View(offers);

        }
    }
}
