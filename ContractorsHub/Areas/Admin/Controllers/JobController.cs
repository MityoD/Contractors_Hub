using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class JobController : Controller
    {
        private readonly IJobAdministrationService service;

        public JobController(IJobAdministrationService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var allJobs = await service.GetAllJobsAsync();

            return View(allJobs);
        }

        public async Task<IActionResult> Active()
        {
            var jobs = await service.ReviewActiveJobs();
            return View(jobs);
        }

        public async Task<IActionResult> Pending()
        {
            var jobs = await service.ReviewPendingJobs();
            return View(jobs);
        }

        public async Task<IActionResult> Declined()
        {
            var jobs = await service.ReviewDeclinedJobs();
            return View(jobs);
        }


        public async Task<IActionResult> Details(int id)
        {
            //if ((await service.JobExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Job");
            //}

            var model = await service.JobDetailsAsync(id);
            return View(model);
        }
        
        public async Task<IActionResult> Approve(int id)
        {
            //if ((await service.JobExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Job");
            //}

            await service.ApproveJobAsync(id);
            var jobs = await service.ReviewPendingJobs();
            return View("Pending", jobs);
        }

        public async Task<IActionResult> Decline(int id)
        {
            //if ((await service.JobExistAsync(id)) == false)
            //{
            //    TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
            //    return RedirectToAction("All", "Job");
            //}

            await service.DeclineJobAsync(id);
            var jobs = await service.ReviewPendingJobs();
            return View("Pending", jobs);
        }


    }
}
