using ContractorsHub.Areas.Admin.Service;
using ContractorsHub.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ContractorsHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class StatisticController : Controller
    {
        private readonly StatisticAdministrationService service;

        public StatisticController(StatisticAdministrationService _service)
        {
            service = _service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> All()
        {
            var model = await service.StatisticAsync();
            return View(model);
        }
    }
}
