using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models;
using ContractorsHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractorsHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IToolService toolService;

        public HomeController(ILogger<HomeController> _logger, IToolService _toolService)
        {
            logger = _logger;
            toolService = _toolService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await toolService.GetLastThreeTools();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}