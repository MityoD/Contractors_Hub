using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Contractor;
using ContractorsHub.Extensions;
using ContractorsHub.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{


    [Authorize]
    public class ContractorController : Controller
    {
        private readonly IContractorService service;
        private readonly ILogger logger;

        public ContractorController(
            IContractorService _service,
            ILogger<ContractorController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await service.AllContractorsAsync();
                return View(model);
            }
            catch (Exception ms)
            {
                //logger log exception
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
           
        }
        //move rateController to contractor ?

        [HttpGet]
        [Authorize(Roles = RoleConstants.Guest)]
        public async Task<IActionResult> Become()
        {
            var model = new BecomeContractorViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Guest)]
        public async Task<IActionResult> Become(BecomeContractorViewModel model)
        {   
            if (!ModelState.IsValid)
            {
                return View(model);                           
            }

            try
            {
                await service.AddContractorAsync(User.Id(), model);
                return RedirectToAction("JoinContractors", "User");
            }
            catch (Exception ms)
            {
                //logger log exception
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
    
        }

        [HttpGet]
        public async Task<IActionResult> RateContractor(string id, int jobId)
        {
            //check if contractor exists
            // if user exist
            var model = new ContractorRatingModel()
            {
                ContractorId = id,
                UserId = User.Id(),
                JobId = jobId
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RateContractor(string id, int jobId, ContractorRatingModel model)
        {
           
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "invalid view!";

                return View(model);
            }
            try
            {
                await service.RateContractorAsync(User.Id(), id, model);

                return RedirectToAction("MyJobs", "Job");
            }
            catch (Exception ms)
            {
                //logger log exception
                logger.LogError(ms.Message, ms);
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
