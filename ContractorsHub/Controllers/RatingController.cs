using ContractorsHub.Constants;
using ContractorsHub.Contracts;
using ContractorsHub.Extensions;
using ContractorsHub.Models.Rating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IRatingService service;
        private readonly ILogger logger;

        public RatingController(IRatingService _service, ILogger<RatingController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        public async Task<IActionResult> Rating(string id)
        {
            try
            {
                var model = await service.GetRatingAsync(id);

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

        [HttpGet]
        public async Task<IActionResult> RateContractor(string id, int jobId)
        {
            //check if contractor exists
            // if user exist
            var model = new RatingModel()
            {
                ContractorId = id,
                UserId = User.Id(),
                JobId = jobId
            };
           
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RateContractor(string id,int jobId, RatingModel model)
        {
            //model.JobId = Convert.ToInt32(TempData["JobId"]);
            //model.ContractorId = (TempData["ContractorId"]).ToString();
            //model.UserId = (TempData["UserId"]).ToString();
   

            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "invalid view!";

                return View(model);
            }
            try
            {
                await service.RateContractorAsync(User.Id(), id,  model);

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
