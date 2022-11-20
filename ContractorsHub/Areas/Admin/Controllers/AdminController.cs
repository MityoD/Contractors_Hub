using ContractorsHub.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ContractorsHub.Areas.Administration.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {   

            return View();
        }
    }
}
