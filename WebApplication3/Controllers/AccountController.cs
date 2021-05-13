using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLy_CircleK.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string pass)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.usernameError = "Nhập username";
            } else if (string.IsNullOrEmpty(pass))
            {
                ViewBag.passwordError = "Nhập Password";
            } else
            {
                if(username.Equals("quocdat") && pass.Equals("quocdat123"))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    return RedirectToAction("Index", "HangHoa");
                } else
                {
                    ViewBag.invalidData = "Nhập username = quocdat và pass = quocdat123";
                }
            }
            ViewBag.username = username;
            return View();
        }
        public ActionResult Logoff() 
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HangHoa");
        }
    }
}