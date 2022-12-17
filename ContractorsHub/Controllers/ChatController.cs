using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public IActionResult LiveChat()
        {
            return View();
        }
    }
}
