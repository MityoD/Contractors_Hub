using ContractorsHub.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
