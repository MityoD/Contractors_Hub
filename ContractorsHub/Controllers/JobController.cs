using ContractorsHub.Constants;
using ContractorsHub.Contracts;
using ContractorsHub.Extensions;
using ContractorsHub.Models;
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
        public IActionResult Add()
        {
            var model = new JobModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobModel model)
        {
            if (!ModelState.IsValid)
            {
               // TempData[MessageConstant.ErrorMessage] = "Invalid Data!";

                return View(model);

            }
            var userId = User.Id();

            await service.AddJobAsync(userId, model);

            TempData[MessageConstant.SuccessMessage] = "Job Added!";
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

            if (model.Owner.Id != User.Id()) 
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                return RedirectToAction("All", "Job");
            }

            if (!ModelState.IsValid)
            {
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
    }
}
