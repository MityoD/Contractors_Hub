using ContractorsHub.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IRatingService service;

        public RatingController(IRatingService _service)
        {
           service = _service;
        }

        public async Task<IActionResult> Rating(string id)
        {
            var model = await service.GetRatingAsync(id);

            return View(model);
        }
    }
}
