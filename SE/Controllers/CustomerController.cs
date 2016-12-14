using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerModel model = new CustomerModel();
        private Notify note = new Notify();

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            ViewBag.sit = id;
            ViewBag.idSchedule = Request.Params["idSchedule"].ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer v)
        {
            ViewBag.sit = Convert.ToInt32(Request.Params["idSit"].ToString());
            ViewBag.idSchedule = Request.Params["idSchedule"].ToString();
            /// Check values input format
            if (CheckCustomer(v) != null)
            {
                ViewBag.notify = CheckCustomer(v);
                return View();
            }

            model.AddCustomer(v);
            return RedirectToAction("Create", "Ticket", new { ID = ViewBag.sit, idSchedule = ViewBag.idSchedule });
        }

        public Notify CheckCustomer(Customer v)
        {
            if (v == null)
            {
                return new Notify { status = false, msg = "Please input customer" };
            }
            else
            { 
                if (note.checkError(v.name, null))
                {
                    return new Notify { status = false, msg = "Please enter name" };
                }
                else if (note.checkError(v.address, null))
                {
                    return new Notify { status = false, msg = "Please enter address" };
                }
                else if (note.checkError(v.phone, "number"))
                {
                    return new Notify { status = false, msg = "Please enter phone" };
                }
                else if (note.checkError(v.idcard, "number"))
                {
                    return new Notify { status = false, msg = "Please enter id card" };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}