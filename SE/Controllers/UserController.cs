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
        private Notify note = new Notify();

        /// <summary>
        /// GET: User
        /// </summary>
        /// <returns></returns>
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
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE");
            if (CheckUser(v) != null)
            {
                ViewBag.notify = CheckUser(v);
                return View();
            }
            model.AddEmployee(v);
            ViewBag.notify = new Notify { status = true, msg = "Add user successfull" };
            return View("Index");
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
                    position = x.EmployeeType.nameTypeE,
                    action = ("<div class=\"todo-list-item\" style=\"padding:0\"><a title=\"Edit\" href=\"" + Url.Action("Edit", new { ID = x.idEmployee })+ "\" ><svg class=\"glyph stroked pencil\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-pencil\"></use></svg></a>&nbsp;&nbsp;<a title=\"Delete\" href = \"" + Url.Action("Delete", new { ID = x.idEmployee }) + "\" class=\"trash\"><svg class=\"glyph stroked trash\" style=\"width:20px; height: 20px\"><use xlink:href=\"#stroked-trash\"></use></svg></a></div>").ToString()
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
            if (CheckUser(v) != null)
            {
                return View("Index");
            }   
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE", v.idTypeE);
            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Employee v)
        {
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE");
            if (CheckUser(v) != null)
            {
                ViewBag.notify = CheckUser(v);
                return View();
            }
            model.Edit(v);
            ViewBag.notify = new Notify { status = true, msg = "Edit user successfull" };
            return View("Index");
        }

        public Notify CheckUser(Employee v)
        {
            if (v == null)
            {
                return new Notify { status = false, msg = "Please input employee" };
            }
            else
            {
                if (note.checkError(v.name, null))
                {
                    return new Notify { status = false, msg = "Please enter your name" };
                }
                else if (note.checkError(v.username, null))
                {
                    return new Notify { status = false, msg = "Please enter your username" };
                }
                else if (note.checkError(v.address, null))
                {
                    return new Notify { status = false, msg = "Please enter your address" };
                }
                else if (note.checkError(v.phone, "number"))
                {
                    return new Notify { status = false, msg = "Please enter your phone" };
                }
                else if (note.checkError(v.idcard, "number"))
                {
                    return new Notify { status = false, msg = "Please enter your idcard" };
                }
                else if (note.checkError(v.birthday, null))
                {
                    return new Notify { status = false, msg = "Please enter your birthday" };
                }
                else if (note.checkError(v.startday, null))
                {
                    return new Notify { status = false, msg = "Please enter your startday" };
                }
                else
                {
                    return null;
                }
            }
        }
    }
}