using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomFormAuthentication.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyProfile()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult UserIndex()
        {
            return View();
        }

    }
}
