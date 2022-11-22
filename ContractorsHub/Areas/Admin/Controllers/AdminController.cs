using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ContractorsHub.Areas.Administration.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        private readonly IAdministrationService service;

        public AdminController(IAdministrationService _service)
        {
            service = _service;
        }

        public IActionResult Index()
        {   

            return View();
        }

        public async Task<IActionResult> All ()
        {
            var allJobs = await service.GetAllJobsAsync();

            return View(allJobs);
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
            var allJobs = await service.GetAllJobsAsync();

            return View("All",allJobs);
        }
    }
}
