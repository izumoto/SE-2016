using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE.Controllers
{
    public class PlaceController : Controller
    {
        private PlaceModel model = new PlaceModel();

        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListCity()
        {
            var data = model.GetListCity();

            JsonResult result = Json(data.Select(
                x => new {
                    id = x.idCity,
                    name = x.nameCity,
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("EditCity", new { ID = x.idCity }) + "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("DeleteCity", new { ID = x.idCity }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
                }), JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult GetListRoute()
        {
            var data = model.GetListRoute();

            JsonResult result = Json(data.Select(
                x => new {
                    id = x.idRoute,
                    name = model.GetNameCity(x.idFrom) + " - " + model.GetNameCity(x.idTo),
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("EditRoute", new { ID = x.idRoute }) + "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("DeleteRoute", new { ID = x.idRoute }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
                }), JsonRequestBehavior.AllowGet);

            return result;
        }

        public ActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCity(City x)
        {
            if (x.nameCity == null)
                return View();

            model.AddCity(x);

            return RedirectToAction("Index");
        }

        public ActionResult CreateRoute()
        {
            ViewBag.idFrom = new SelectList(model.GetListCity(), "idCity", "nameCity");
            ViewBag.idTo = new SelectList(model.GetListCity(), "idCity", "nameCity");

            return View();
        }

        [HttpPost]
        public ActionResult CreateRoute(Route x)
        {
            if (x.idFrom == null | x.idTo == null)
                return View();

            model.AddRoute(x);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCity(int id)
        {
            model.DeleteCity(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditCity(int id)
        {
            var v = model.EditCity(id);

            return View(v);
        }

        [HttpPost]
        public ActionResult EditCity(City v)
        {
            if (v.nameCity == null)
                return View();

            model.EditCity(v);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteRoute(int id)
        {
            model.DeleteRoute(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditRoute(int id)
        {
            var v = model.EditRoute(id);

            ViewBag.idFrom = new SelectList(model.GetListCity(), "idCity", "nameCity", v.idFrom);
            ViewBag.idTo = new SelectList(model.GetListCity(), "idCity", "nameCity", v.idTo);

            return View(v);
        }

        [HttpPost]
        public ActionResult EditRoute(Route v)
        {
            if (v.idFrom == null | v.idTo == null)
                return View();

            model.EditRoute(v);

            return RedirectToAction("Index");
        }
    }
}