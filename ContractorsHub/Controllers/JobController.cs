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

        public JobController(IJobService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {


            var model = new JobModel()
            {
                JobCategories = await service.AllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobModel model)
        {

            if ((await service.CategoryExists(model.CategoryId)) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exists");
            }

            if (!ModelState.IsValid)
            {
                // TempData[MessageConstant.ErrorMessage] = "Invalid Data!";
                model.JobCategories = await service.AllCategories();

                return View(model);

            }
            var userId = User.Id();

            await service.AddJobAsync(userId, model);

            TempData[MessageConstant.SuccessMessage] = "Job send for review!";
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var jobs = await service.GetAllJobsAsync();
            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await service.JobExistAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("All", "Job");
            }           

            var model = await service.GetEditAsync(id);

            if (model.Owner?.Id != User.Id())  // owner pops null
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("All", "Job");
            }
            model.JobCategories = await service.AllCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, JobModel model)
        {
            if ((await service.JobExistAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("All", "Job");
            }

            //if (model.Owner?.Id != User.Id())  // owner throws null
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Job");
            //}

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
            return RedirectToAction("All", "Job");        

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if ((await service.JobExistAsync(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("All", "Job");
            }

            var model = await service.JobDetailsAsync(id);
            return View(model);
        }

        public async Task<IActionResult> JobOffers()
        {
            var model = await service.JobOffersAsync(User.Id());

            return View(model); 
        }

        public async Task<IActionResult> MyJobs()
        {
            try
            {
                 var model = await service.GetMyJobsAsync(User.Id());
                 return View(model);
            }
            catch (Exception)
            {   //logger log exception
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<IActionResult> Complete(int id)
        {
            try
            {
                string contractorId = await service.CompleteJob(id, User.Id());
                return RedirectToAction("RateContractor","Contractor", new { id = contractorId, jobId = id });
            }
            catch (Exception)
            {   //logger log exception
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
