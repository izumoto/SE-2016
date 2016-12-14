using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SE.Models;

namespace SE.Controllers
{
    public class HomeController : Controller
    {
        private HomeModel model = new HomeModel();

        /// <summary>
        /// GET Home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(model);
        }
    }
}