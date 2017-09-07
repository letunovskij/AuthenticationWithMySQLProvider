using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationWithMySQLProvider.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString)
        {
            var users = UserManager.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.ApplicationUserInfo.FullName.Contains(searchString));
                var cnt = users.Count();
            }

            return View(users);
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

    }
}