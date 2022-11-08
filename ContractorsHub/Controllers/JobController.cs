using ContractorsHub.Contracts;
using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
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
            var model = new AddJobViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddJobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            await service.AddJobAsync(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var jobs = await service.GetAllJobsAsync();
            return View(jobs);
        }
    }
}
