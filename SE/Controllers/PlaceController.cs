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
        private Notify note = new Notify();

        /// <summary>
        /// GET: Route
        /// </summary>
        /// <returns></returns>
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
                    name = x.City.nameCity + " - " + x.City1.nameCity,
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("EditRoute", new { ID = x.idRoute }) + "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("DeleteRoute", new { ID = x.idRoute }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
                }), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCity(City v)
        {
            if (note.checkError(v.nameCity, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your city name" };
                return View();
            }
            model.AddCity(v);
            ViewBag.notify = new Notify { status = true, msg = "Add city successfull" };
            return View("Index");
        }

        public ActionResult CreateRoute()
        {
            ViewBag.idFrom = new SelectList(model.GetListCity(), "idCity", "nameCity");
            ViewBag.idTo = new SelectList(model.GetListCity(), "idCity", "nameCity");
            return View();
        }

        [HttpPost]
        public ActionResult CreateRoute(Route v)
        {
            if (note.checkError(v.idFrom, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select city from" };
                return View();
            }
            else if (note.checkError(v.idTo, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select city to" };
                return View();
            }
            model.AddRoute(v);
            ViewBag.notify = new Notify { status = true, msg = "Add route successfull" };
            return View("Index");
        }

        public ActionResult DeleteCity(int id)
        {
            model.DeleteCity(id);
            return View("Index");
        }

        public ActionResult EditCity(int id)
        {
            var v = model.EditCity(id);
            if (note.checkError(v, null))
            {
                return View("Index");
            }  
            return View(v);
        }

        [HttpPost]
        public ActionResult EditCity(City v)
        {
            if (note.checkError(v.nameCity, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your city name" };
                return View();
            }
            model.EditCity(v);
            ViewBag.notify = new Notify { status = true, msg = "Edit city successfull" };
            return View("Index");
        }

        public ActionResult DeleteRoute(int id)
        {
            model.DeleteRoute(id);
            return View("Index");
        }

        public ActionResult EditRoute(int id)
        {
            var v = model.EditRoute(id);
            if (note.checkError(v, null))
            {
                return View("Index");
            }
            ViewBag.idFrom = new SelectList(model.GetListCity(), "idCity", "nameCity", v.idFrom);
            ViewBag.idTo = new SelectList(model.GetListCity(), "idCity", "nameCity", v.idTo);
            return View(v);
        }

        [HttpPost]
        public ActionResult EditRoute(Route v)
        {
            if (note.checkError(v.idFrom, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select city from" };
                return View();
            }
            else if (note.checkError(v.idTo, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please select city to" };
                return View();
            }
            model.EditRoute(v);
            ViewBag.notify = new Notify { status = true, msg = "Edit route successfull" };
            return View("Index");
        }
    }
}