using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    public class OfferController : Controller
    {
        [HttpPost]
        public IActionResult Send()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
