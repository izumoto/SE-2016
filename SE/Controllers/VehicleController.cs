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
        public ActionResult Create(Vehicle x)
        {
            if (x.license == null | x.dayImport == null)
                return View();

            model.AddVehicle(x);

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var v = model.Edit(id);

            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Vehicle v)
        {
            if (v.license == null | v.dayImport == null)
                return View();

            model.Edit(v);

            return RedirectToAction("Index");
        }
    }
}