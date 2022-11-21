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
            var userId = User.Id();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[MessageConstant.ErrorMessage] = "Invalid Data!";

                return View(model);

            }
            var userId = User.Id();

            await service.AddJobAsync(userId, model);

            TempData[MessageConstant.SuccessMessage] = "Job Added!";
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var jobs = await service.GetAllJobsAsync();
            return View(jobs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await service.GetEditAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, JobModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            await service.PostEditAsync(id, model);
            return RedirectToAction("All", "Job");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await service.JobDetailsAsync(id);
            return View(model);
        }
    }
}
