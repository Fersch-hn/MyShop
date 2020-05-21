using Microsoft.AspNet.Identity.Owin;
using MyShop.Core.Models.Auth;
using MyShop.Services.Auth.Service;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Common;

namespace MyShop.WebUI.Controllers
{
  

    [Authorize]
    public class UserController : Controller
    {
        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        private ApplicationUserManager _userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private readonly IUserService _userService = DependecyFactory.GetInstance<IUserService>();

        // GET: User
        public ActionResult Index()
        {
            var model = _userService.GetAll();

            return View(model);
        }

        public async Task<ActionResult> Get(string id)
        {
            var model = await _userManager.FindByIdAsync(id);
            ViewBag.Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList();

            return View(model);
        }

        public async Task<ActionResult> AddRole(ApplicationUserRole role)
        {
            if (!_userManager.IsInRoleAsync(role.UserId, role.RoleId).Result)
            {
                var result = await _userManager.AddToRoleAsync(role.UserId, role.RoleId);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First());
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public string Admin()
        {
            return "Admin";
        }

        [AllowAnonymous]
        public string Anonymous()
        {
            return "Anonymous";
        }
    }
}