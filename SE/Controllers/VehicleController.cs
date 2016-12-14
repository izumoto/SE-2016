using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE.Controllers
{
    public class VehicleController : Controller
    {
        private VehicleModel model = new VehicleModel();
        private Notify note = new Notify();

        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vehicle v)
        {
            if (note.checkError(v.license, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your license" };
                return View();
            }
            else if (note.checkError(v.dayImport, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your day import" };
                return View();
            }

            model.AddVehicle(v);

            ViewBag.notify = new Notify { status = true, msg = "Add vehicle successfull" };

            return View("Index");
        }

        public JsonResult GetListVehicle()
        {
            var data = model.GetListVehicle();

            JsonResult result = Json(data.Select(
                x => new {
                    id = x.idVehicle,
                    linces = x.license,
                    date = x.dayImport.ToString(),
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("Edit", new { ID = x.idVehicle }) + "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("Delete", new { ID = x.idVehicle }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
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
                return View("Index");

            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Vehicle v)
        {
            if (note.checkError(v.license, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your license" };
                return View();
            }
            else if (note.checkError(v.dayImport, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your day import" };
                return View();
            }

            model.Edit(v);

            ViewBag.notify = new Notify { status = true, msg = "Edit vehicle successfull" };

            return View("Index");
        }
    }
}