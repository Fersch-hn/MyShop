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


namespace MyShop.WebUI.Controllers
{
    //[Authorize]
    //public class UserController : Controller
    //{
    //    private readonly UserService _userService = new UserService();

    //    private ApplicationRoleManager _roleManager
    //    {
    //        get
    //        {
    //            return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
    //        }
    //    }

    //    private ApplicationUserManager _userManager
    //    {
    //        get
    //        {
    //            return HttpContext.GetOwinContext().Get<ApplicationUserManager>();
    //        }
    //    }

    //    public ActionResult Index()
    //    {
    //        return View(
    //            _userService.GetAll()
    //            );
    //    }

    //    public ActionResult Get(string id)
    //    {
    //        ViewBag.Roles = _roleManager.Roles.Where(x => x.Enabled).ToList();
    //        return View(
    //            _userService.Get(id)
    //            );
    //    }

    //    public async Task<ActionResult> AddRoleToUser(string Id, string role)
    //    {
    //        var roles = await _userManager.GetRolesAsync(Id);

    //        if (roles.Any())
    //        {
    //            throw new Exception("Solo se puede tener un rol por usuario");
    //        }

    //        await _userManager.AddToRoleAsync(Id, role);
    //        return RedirectToAction("Index");
    //    }

    //    public async Task CreateRoles()
    //    {
    //        var roles = new List<ApplicationRole>
    //        {
    //            new ApplicationRole {Name = "Admin"},
    //            new ApplicationRole {Name = "Moderator"},
    //            new ApplicationRole {Name = "User"},
    //        };

    //        foreach (var r in roles)
    //        {
    //            if (!await _roleManager.RoleExistsAsync(r.Name))
    //            {
    //                await _roleManager.CreateAsync(r);
    //            }

    //        }
    //    }

    //    [AllowAnonymous]
    //    public string Anonymous()
    //    {
    //        return "Anonymous";
    //    }

    //    [Authorize(Roles = "Guest")]
    //    public string Guest()
    //    {
    //        return "Guest";
    //    }

    //    [Authorize(Roles = "Moderator")]
    //    public string Moderator()
    //    {
    //        return "Moderator";
    //    }

    //    [Authorize(Roles = "Admin")]
    //    public string Admin()
    //    {
    //        return "Admin";
    //    }
    //}

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

        private readonly UserService _userService = new UserService();

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
    }
}