using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE.Controllers
{
    public class ScheduleController : Controller
    {
        private VehicleModel vehicle = new VehicleModel();
        private PlaceModel route = new PlaceModel();
        private UserModel user = new UserModel();
        private ScheduleModel model = new ScheduleModel();
        private Notify note = new Notify();

        /// <summary>
        /// GET: Schedule
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.idTime = new SelectList(model.GetListTime(), "idTime", "time");
            ViewBag.idRoute = new SelectList(route.GetListRoute().Select(x => new { idRoute = x.idRoute, nameRoute = x.City.nameCity + " - " + x.City1.nameCity}), "idRoute", "nameRoute");
            ViewBag.idVehicle = new SelectList(vehicle.GetListVehicle(), "idVehicle", "license");
            ViewBag.idEmployee = new SelectList(user.GetListUser(3), "idEmployee", "name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Schedule v)
        {
            ViewBag.idTime = new SelectList(model.GetListTime(), "idTime", "time");
            ViewBag.idRoute = new SelectList(route.GetListRoute().Select(x => new { idRoute = x.idRoute, nameRoute = x.City.nameCity + " - " + x.City1.nameCity }), "idRoute", "nameRoute");
            ViewBag.idVehicle = new SelectList(vehicle.GetListVehicle(), "idVehicle", "license");
            ViewBag.idEmployee = new SelectList(user.GetListUser(3), "idEmployee", "name");
            if (note.checkError(v.dayStart, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter day start" };
                return View();
            }
            else if (note.checkError(v.dayEnd, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter day end" };
                return View();
            }
            else if (note.checkError(v.idRoute, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select route" };
                return View();
            }
            else if (note.checkError(v.idTime, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select time" };
                return View();
            }
            else if (note.checkError(v.idEmployee, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select driver" };
                return View();
            }
            else if (note.checkError(v.idVehicle, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select vehicle" };
                return View();
            }
            else if (note.checkError(v.price, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter price" };
                return View();
            }

            model.AddSchedule(v);
            ViewBag.notify = new Notify { status = true, msg = "Add schedule successfull" };
            return View("Index");
        }
        
        public JsonResult GetListSchedule()
        {
            string date = "";
            if (Request.Params["date"] != null)
            {
                date = Request.Params["date"].ToString();
            }     
                    
            var data = model.GetListSchedule(date);
            JsonResult result = Json(data.Select(
                x => new
                {
                    id = x.idSchedule,
                    date = x.dayStart + " - " + x.dayEnd,
                    route = x.Route.City.nameCity + " - " + x.Route.City1.nameCity,
                    vehicle = x.Vehicle.license,
                    price = x.price,
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("Edit", new { ID = x.idSchedule }) + "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("Delete", new { ID = x.idSchedule }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString(),
                    choose = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Choose\" href=\"" + Url.Action("Choose", "Ticket", new { ID = x.idSchedule }) + "\" ><svg class=\"glyph stroked checkmark\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-checkmark\"></use></svg>Choose</a>").ToString()
                }), JsonRequestBehavior.AllowGet);

            return result;
        }

        public ActionResult Delete(int id)
        {
            model.Delete(id);
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            var v = model.Edit(id);
            if (note.checkError(v, null))
            {
                return View("Index");
            }  
              
            ViewBag.idTime = new SelectList(model.GetListTime(), "idTime", "time", v.idTime);
            ViewBag.idRoute = new SelectList(route.GetListRoute().Select(x => new { idRoute = x.idRoute, nameRoute = x.City.nameCity + " - " + x.City1.nameCity }), "idRoute", "nameRoute", v.idRoute);
            ViewBag.idVehicle = new SelectList(vehicle.GetListVehicle(), "idVehicle", "license", v.idVehicle);
            ViewBag.idEmployee = new SelectList(user.GetListUser(3), "idEmployee", "name", v.idEmployee);
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Schedule v)
        {
            ViewBag.idTime = new SelectList(model.GetListTime(), "idTime", "time");
            ViewBag.idRoute = new SelectList(route.GetListRoute().Select(x => new { idRoute = x.idRoute, nameRoute = x.City.nameCity + " - " + x.City1.nameCity }), "idRoute", "nameRoute");
            ViewBag.idVehicle = new SelectList(vehicle.GetListVehicle(), "idVehicle", "license");
            ViewBag.idEmployee = new SelectList(user.GetListUser(3), "idEmployee", "name");
            if (note.checkError(v.dayStart, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter day start" };
                return View();
            }
            else if (note.checkError(v.dayEnd, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter day end" };
                return View();
            }
            else if (note.checkError(v.idRoute, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select route" };
                return View();
            }
            else if (note.checkError(v.idTime, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select time" };
                return View();
            }
            else if (note.checkError(v.idEmployee, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select driver" };
                return View();
            }
            else if (note.checkError(v.idVehicle, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select vehicle" };
                return View();
            }
            else if (note.checkError(v.price, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter price" };
                return View();
            }

            model.Edit(v);
            ViewBag.notify = new Notify { status = true, msg = "Edit schedule successfull" };
            return View("Index");
        }
    }
}