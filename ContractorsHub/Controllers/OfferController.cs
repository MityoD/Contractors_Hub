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
        private readonly ILogger<OfferController> logger;

        public OfferController(IOfferService _service,ILogger<OfferController> _logger)
        {
            service = _service;
            logger = _logger;
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await service.SendOfferAsync(model, id, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Offer sent";
                return RedirectToAction("All", "Job");
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                logger.LogError(ms.Message, ms);
                return RedirectToAction("All","Job");
            }

    
        }
     


        [HttpGet]
        [Authorize(Roles = RoleConstants.Contractor)]
        public async Task<IActionResult> OffersCondition()
        {
            try
            {
                var offersCondition = await service.OffersConditionAsync(User.Id());
                return View(offersCondition);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
           
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Administrator}, {RoleConstants.Guest}")]
        public async Task<IActionResult> Accept(int id)
        {
            try
            {
                await service.AcceptOfferAsync(id);
                return RedirectToAction("JobOffers", "Job");
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }


        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Administrator}, {RoleConstants.Guest}")]
        public async Task<IActionResult> Decline(int id)
        {
            try
            {
                if (await service.OfferExists(id))
                {
                    var offer = await service.GetOfferAsync(id);
                    await service.DeclineOfferAsync(id);
                }
                return RedirectToAction("JobOffers", "Job");
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }          
        }
        [HttpGet]
        [Authorize(Roles = RoleConstants.Contractor)]
        public async Task<IActionResult> Delete(string id, int offerId)
        {        
            try
            {
                await service.RemoveOfferAsync(id, offerId);
                return RedirectToAction(nameof(OffersCondition));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(OffersCondition));
            }
        }
    }
}
