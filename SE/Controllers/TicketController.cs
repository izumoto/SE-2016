using SE.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE.Controllers
{
    public class TicketController : Controller
    {
        private TicketModel model = new TicketModel();
        private CustomerModel customer = new CustomerModel();
        private Notify note = new Notify();

        /// <summary>
        /// GET: Ticket
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["schedule"] != null)
            {
                int idSchedule = ((ListTicket)Session["schedule"]).idSchedule;
                List<Ticket> ticket = model.GetListTicket(Convert.ToInt32(idSchedule));
                Session["schedule"] = new ListTicket { idSchedule = Convert.ToInt32(idSchedule), ticket = ticket };
            }   
                 
            return View();
        }

        public ActionResult Create(int id)
        {
            ViewBag.sit = id;
            ViewBag.idSchedule = Request.Params["idschedule"].ToString();
            ViewBag.idStatus = new SelectList(model.GetListStatus(), "idStatus", "nameStatus");
            ViewBag.idCustomer = new SelectList(customer.GetListCustomer().Select(x => new { idCustomer = x.idCustomer, name = x.name + " - " + x.phone }), "idCustomer", "name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ticket v)
        {
            model.AddTicket(v);
            List<Ticket> ticket = model.GetListTicket(Convert.ToInt32(v.idSchedule));
            Session["schedule"] = new ListTicket { idSchedule = Convert.ToInt32(v.idSchedule), ticket = ticket };
            ViewBag.notify = new Notify { status = true, msg = "Sell ticket successfull" };
            return View("Index");
        }

        [HttpPost]
        public ActionResult SetDate(string date)
        {
            Session["date"] = Request.Params["dayStart"].ToString();
            return RedirectToAction("Index");
        }

        public ActionResult Choose(int id)
        {
            List<Ticket> ticket = model.GetListTicket(id);
            Session["schedule"] = new ListTicket{ idSchedule = id, ticket = ticket };
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            model.Delete(id);
            int idSchedule = ((ListTicket)Session["schedule"]).idSchedule;
            List<Ticket> ticket = model.GetListTicket(Convert.ToInt32(idSchedule));
            Session["schedule"] = new ListTicket { idSchedule = Convert.ToInt32(idSchedule), ticket = ticket };
            return RedirectToAction("Index");
        }

        public ActionResult Paid(int id)
        {
            var v = model.Paid(id);
            if (v == true)
            {
                int idSchedule = ((ListTicket)Session["schedule"]).idSchedule;
                List<Ticket> ticket = model.GetListTicket(Convert.ToInt32(idSchedule));
                Session["schedule"] = new ListTicket { idSchedule = Convert.ToInt32(idSchedule), ticket = ticket };
                ViewBag.notify = new Notify { status = true, msg = "Paid successfull" };
            }
            else
            {
                ViewBag.notify = new Notify { status = false, msg = "Failed to paid" };
            }

            return View("Index");
        }
    }
}