using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Extensions;
using ContractorsHub.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        private readonly IJobService service;
        private readonly ILogger<JobController> logger;

        public JobController(IJobService _service, ILogger<JobController> _logger)
        {
            service = _service;
            logger = _logger;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new JobModel()
                {
                    JobCategories = await service.AllCategories()
                };
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Add(JobModel model)
        {

            if ((await service.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
            }

            if (!ModelState.IsValid)
            {
                model.JobCategories = await service.AllCategories();
                return View(model);

            }
            try
            {
                var userId = User.Id();
                await service.AddJobAsync(userId, model);
                TempData[MessageConstant.SuccessMessage] = "Job send for review!";
                return RedirectToAction(nameof(MyJobs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var jobs = await service.GetAllJobsAsync();
                return View(jobs);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }         
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await service.GetEditAsync(id, User.Id());
                model.JobCategories = await service.AllCategories();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(MyJobs));
            }           
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Edit(int id, JobModel model)
        {
            try
            {
                if ((await service.CategoryExists(model.CategoryId)) == false)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
                    model.JobCategories = await service.AllCategories();
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    model.JobCategories = await service.AllCategories();
                    return View(model);

                }

                await service.PostEditAsync(id, model);
                return RedirectToAction(nameof(MyJobs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(MyJobs));
            }
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if ((await service.JobExistAsync(id)) == false)
                {
                    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                    return RedirectToAction("All", "Job");
                }

                var model = await service.JobDetailsAsync(id);
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }            
        }

        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> JobOffers()
        {
            var model = await service.JobOffersAsync(User.Id());

            return View(model); 
        }

        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> MyJobs()
        {
            try
            {
                 var model = await service.GetMyJobsAsync(User.Id());
                 return View(model);
            }
            catch (Exception)
            {   
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                string contractorId = await service.CompleteJob(id, User.Id());
                return RedirectToAction("RateContractor","Contractor", new { id = contractorId, jobId = id });
            }
            catch (Exception)
            {   
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = $"{RoleConstants.Guest}, {RoleConstants.Administrator}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteJobAsync(id, User.Id());
                return RedirectToAction(nameof(MyJobs));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                return RedirectToAction(nameof(MyJobs));
            }
        }
    }
}
