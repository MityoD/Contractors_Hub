using ContractorsHub.Constants;
using ContractorsHub.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ContractorsHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.Administrator)]
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;

        public UserController(SignInManager<User> _signInManager)
        {
            signInManager = _signInManager;
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
