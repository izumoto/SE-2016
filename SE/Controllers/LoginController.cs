using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SE.Models;

namespace SE.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// GET: Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            var user = model.CheckUser();
            if (user == null)
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter correct Username and Password." };
                return View();
            }

            Session["user"] = user;
            return Redirect(Url.Action("Index", "Home"));
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Login");
        }

        public static bool RoleUser(String action, Employee user)
        {
            switch (action)
            {
                case "User":
                    return !(user.EmployeeType.nameTypeE == "Driver" | user.EmployeeType.nameTypeE == "Ticket Seller");
                case "Vehicle":
                    return !(user.EmployeeType.nameTypeE == "Driver" | user.EmployeeType.nameTypeE == "Ticket Seller");
                case "Place":
                    return !(user.EmployeeType.nameTypeE == "Driver" | user.EmployeeType.nameTypeE == "Ticket Seller");
                case "Schedule":
                    return true;
                case "Ticket":
                    return !(user.EmployeeType.nameTypeE == "Driver");
                default:
                    return true;
            }
        }
    }
}