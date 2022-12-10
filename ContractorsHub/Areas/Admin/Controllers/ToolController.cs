using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Tool;
using ContractorsHub.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Areas.Admin.Controllers
{
    public class ToolController : BaseController
    {
        private readonly IToolService service;
        private readonly IAdminToolService adminService;
        private readonly ILogger<ToolController> logger;

        public ToolController(IToolService _service, IAdminToolService _adminService, ILogger<ToolController> _logger)
        {
            service = _service;
            adminService = _adminService;
            logger = _logger;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var model = new ToolModel();
                model.ToolCategories = await adminService.AllCategories();
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
                    TempData[MessageConstant.ErrorMessage] = "Invalid model data!";
                    model.ToolCategories = await adminService.AllCategories();
                    return View(model);

                }

                await adminService.AddToolAsync(model, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Tool added successfully";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }

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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await adminService.GetEditAsync(id, User.Id());
                model.ToolCategories = await adminService.AllCategories();
                return View(model);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ToolModel model)
        {
            try
            {
                if ((await adminService.CategoryExists(model.CategoryId)) == false)
                {
                    ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
                    model.ToolCategories = await adminService.AllCategories();
                    return View(model);
                }

                if (!ModelState.IsValid)
                {
                    model.ToolCategories = await adminService.AllCategories();
                    return View(model);

                }

                await adminService.PostEditAsync(id, model);
                TempData[MessageConstant.SuccessMessage] = "Tool edited successfully";

                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction(nameof(All));
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await adminService.RemoveToolAsync(id, User.Id());
                TempData[MessageConstant.SuccessMessage] = "Tool removed successfully";
                return RedirectToAction(nameof(All));
            }
            catch (Exception ms)
            {
                logger.LogError(ms.Message, ms);
                TempData[MessageConstant.ErrorMessage] = ms.Message;
                return RedirectToAction(nameof(All));
            }
        }
    }
}
