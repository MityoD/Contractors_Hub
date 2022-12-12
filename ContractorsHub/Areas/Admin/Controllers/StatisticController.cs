using ContractorsHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Areas.Admin.Controllers
{
    public class StatisticController : BaseController
    {
        private readonly StatisticAdministrationService service;

        public StatisticController(StatisticAdministrationService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var model = await service.StatisticAsync();
            return View(model);
        }
    }
}
