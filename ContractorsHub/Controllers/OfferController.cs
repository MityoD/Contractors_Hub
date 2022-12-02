using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Offer;
using ContractorsHub.Extensions;
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
        [Authorize(Roles = RoleConstants.Contractor)]
        public async Task<IActionResult> MyOffers()
        {
           var offers = await service.MyOffersAsync(User.Id());

            return View(offers);

        }


        [HttpGet]
        [Authorize(Roles = RoleConstants.Contractor)]
        public async Task<IActionResult> OffersCondition()
        {
            var offersCondition = await service.OffersConditionAsync(User.Id());

            return View(offersCondition);
        }

        [HttpGet]
        //[Authorize(Roles = $"{RoleConstants.Contractor}, {RoleConstants.Guest}")]
        public async Task<IActionResult> Accept(int id)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            await service.AcceptOfferAsync(id);
            return RedirectToAction("JobOffers","Job");

        }


        [HttpGet]
        //[Authorize(Roles = $"{RoleConstants.Contractor}, {RoleConstants.Guest}")]
        public async Task<IActionResult> Decline(int id)
        {
            if (await service.OfferExists(id))
            {
                var offer = await service.GetOfferAsync(id);
                await service.DeclineOfferAsync(id);
            }
            return RedirectToAction("JobOffers", "Job");
        }


    }
}
