using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Tool;
using Microsoft.AspNetCore.Mvc;
using ContractorsHub.Extensions;
using Microsoft.AspNetCore.Authorization;
using ContractorsHub.Core.Constants;

namespace ContractorsHub.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new ToolModel();
                model.ToolCategories = await service.AllCategories();
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
        public async Task<IActionResult> Add(ToolModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.ToolCategories = await service.AllCategories();
                    return View(model);

                }

                await service.AddToolAsync(model, User.Id());
                return RedirectToAction("Index", "Home");
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
