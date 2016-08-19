using CustomFormAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CustomFormAuthentication.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //using (DatabaseEntities dc = new DatabaseEntities())
            //{
            //    var user = dc.Users.Where(u => u.Username.Equals(model.Username) && u.Password.Equals(model.Password)).FirstOrDefault();

            //    if (user != null)
            //    {
            //        FormsAuthentication.SetAuthCookie(user.Username, model.RememberMe);
            //        if (Url.IsLocalUrl(returnUrl))
            //            return Redirect(returnUrl);
            //        else
            //            return RedirectToAction("MyProfile", "Home");
            //    }
            //}

            //if (ModelState.IsValid)
            //{
            //    var isValidUser = Membership.ValidateUser(model.Username, model.Password);
            //    if (isValidUser)
            //    {
            //        FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
            //        if (Url.IsLocalUrl(returnUrl))
            //            return Redirect(returnUrl);
            //        else
            //            return RedirectToAction("MyProfile", "Home");
            //    }
            //}

            if (ModelState.IsValid)
            {
                bool isValidUser = Membership.ValidateUser(model.Username, model.Password);
                if (isValidUser)
                {
                    User user = null;
                    using (DatabaseEntities dc = new DatabaseEntities())
                    {
                        user = dc.Users.Where(u => u.Username.Equals(model.Username)).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string data = js.Serialize(user);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                            1, 
                            user.Username, 
                            DateTime.Now, 
                            DateTime.Now.AddMinutes(30), 
                            model.RememberMe, 
                            data);
                        string encToken = FormsAuthentication.Encrypt(ticket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encToken);
                        Response.Cookies.Add(authCookie);
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("MyProfile", "Home");
                    }
                }
            }

            ModelState.Remove("Password");
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
