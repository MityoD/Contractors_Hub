using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [AllowAnonymous]
    public class ToolController : Controller
    {
        private readonly IToolService service;
        private readonly ILogger<ToolController> logger;

        public ToolController(IToolService _service, ILogger<ToolController> _logger)
        {
            service = _service;
            logger = _logger;
        }        

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var tools = await service.GetAllToolsAsync();
                return View(tools);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }          
        }

    }
}
