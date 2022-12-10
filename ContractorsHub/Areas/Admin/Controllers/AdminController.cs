using ContractorsHub.Areas.Admin.Controllers;
using ContractorsHub.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractorsHub.Areas.Administration.Controllers
{
   
    public class AdminController : BaseController
    {     

        public IActionResult Index()
        {   

            return View();
        }
        
    }
}
