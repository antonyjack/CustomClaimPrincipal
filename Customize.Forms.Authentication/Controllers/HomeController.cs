using Customize.Forms.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Customize.Forms.Authentication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ClaimsPrincipal principal = User as ClaimsPrincipal;
            string Name = principal.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            string FullName = principal.Claims
                .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fullname").Select(x => x.Value).FirstOrDefault();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}