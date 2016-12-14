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
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE");

            if (note.checkError(v.name, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your name" };              
                return View();
            }
            else if (note.checkError(v.username, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your username" };
                return View();
            }
            else if (note.checkError(v.password, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your password" };
                return View();
            }
            else if (note.checkError(v.address, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your address" };
                return View();
            }
            else if (note.checkError(v.phone, "number"))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your phone" };
                return View();
            }
            else if (note.checkError(v.idcard, "number"))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your idcard" };
                return View();
            }
            else if (note.checkError(v.birthday, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your birthday" };
                return View();
            }
            else if (note.checkError(v.startday, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your startday" };
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

            if (note.checkError(v, null))
                return View("Index");

            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE", v.idTypeE);

            return View(v);
        }

        [HttpPost]
        public ActionResult Edit(Employee v)
        {
            ViewBag.idTypeE = new SelectList(model.GetListPos(), "idTypeE", "nameTypeE");

            if (note.checkError(v.name, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your name" };
                return View();
            }
            else if (note.checkError(v.username, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your username" };
                return View();
            }
            else if (note.checkError(v.address, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your address" };
                return View();
            }
            else if (note.checkError(v.phone, "number"))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your phone" };
                return View();
            }
            else if (note.checkError(v.idcard, "number"))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your idcard" };
                return View();
            }
            else if (note.checkError(v.birthday, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your birthday" };
                return View();
            }
            else if (note.checkError(v.startday, null))
            {
                ViewBag.notify = new Notify { status = false, msg = "Please enter your startday" };
                return View();
            }

            model.Edit(v);

            ViewBag.notify = new Notify { status = true, msg = "Edit user successfull" };

            return View("Index");
        }
        
    }
}