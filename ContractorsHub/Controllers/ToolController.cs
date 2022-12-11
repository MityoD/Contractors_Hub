using ContractorsHub.Core.Constants;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Core.Models.Tool;
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
        public async Task<IActionResult> All([FromQuery] AllToolsQueryModel query)
        {      
            try
            {
                var result = await service.AllToolsAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllToolsQueryModel.ToolsPerPage);

                query.TotalToolsCount = result.TotalToolsCount;
                query.Categories = await service.AllCategoriesNames();
                query.Tools = result.Tools;

                return View(query);
            }
            catch (Exception ms)
            {
                TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
                logger.LogError(ms.Message, ms);
                return RedirectToAction("Index", "Home");
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //    try
        //    {
        //        var tools = await service.GetAllToolsAsync();
        //        return View(tools);
        //    }
        //    catch (Exception ms)
        //    {
        //        TempData[MessageConstant.ErrorMessage] = "Something went wrong!";
        //        logger.LogError(ms.Message, ms);
        //        return RedirectToAction("Index", "Home");
        //    }          
        //}

    }
}
