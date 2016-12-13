using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SE.Models;

namespace SE.Controllers
{
    public class UserController : Controller
    {
        private UserModel model = new UserModel();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee v)
        {
            if (v.name == null | v.username == null | v.password == null | v.name == null | v.address == null | v.phone == null | v.idcard == null)
                return View();

            model.AddEmployee(v);

            return RedirectToAction("Index");
        }

        public JsonResult GetListUser()
        {
            var data = model.GetListUser();
            
            JsonResult result = Json(data.Select(
                x => new {
                    id = x.idEmployee,
                    username = x.username,
                    name = x.name,
                    sex = (x.sex == false) ? "Female" : "Male",
                    position = model.GetPosition(x.idTypeE),
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("Edit", new { ID = x.idEmployee })+ "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("Delete", new { ID = x.idEmployee }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
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
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE", v.idTypeE);
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Employee v)
        {
            if (v.name == null | v.username == null | v.name == null | v.address == null | v.phone == null | v.idcard == null)
                return View();

            model.Edit(v);

            return RedirectToAction("Index");
        }
    }
}