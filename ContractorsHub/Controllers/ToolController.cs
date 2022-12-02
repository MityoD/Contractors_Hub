using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Tool;
using Microsoft.AspNetCore.Mvc;
using ContractorsHub.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class ToolController : Controller
    {
        private readonly IToolService service;

        public ToolController(IToolService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ToolModel();

            model.ToolCategories = await service.AllCategories();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(ToolModel model)
        {

            if (!ModelState.IsValid)
            {
                //categories!
                model.ToolCategories = await service.AllCategories();
                return View(model);

            }

            await service.AddToolAsync(model, User.Id());
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var tools = await service.GetAllToolsAsync();
            return View(tools);
        }

    }
}
