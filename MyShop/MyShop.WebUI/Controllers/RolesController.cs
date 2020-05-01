using Microsoft.AspNet.Identity.Owin;
using Models.Auth;
using MyShop.Services.Auth.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationRoleManager _roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        // GET: User
        public ActionResult Index()
        {
            var model = _roleManager.Roles.OrderBy(x => x.Name).ToList();

            return View(model);
        }

        public async Task<ActionResult> Add(ApplicationRole model)
        {
            var response = await _roleManager.CreateAsync(model);

            if (!response.Succeeded)
            {
                throw new Exception(response.Errors.First());
            }

            return RedirectToAction("Index");
        }
    }
}