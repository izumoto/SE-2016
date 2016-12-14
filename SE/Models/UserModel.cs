using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class UserModel
    {
        private DBDataContext db = new DBDataContext();

        public List<Employee> GetListUser()
        {
            var data = from s in db.Employees select s;

            List<Employee> list = data.ToList<Employee>();

            return list;
        }

        public List<Employee> GetListUser(int id)
        {
            var data = from s in db.Employees where s.idTypeE == id select s;

            List<Employee> list = data.ToList<Employee>();

            return list;
        }

        public List<EmployeeType> GetListPos()
        {
            var data = from s in db.EmployeeTypes select s;

            List<EmployeeType> list = data.ToList<EmployeeType>();

            return list;
        }

        public void AddEmployee(Employee x)
        {
            x.password = db.md5(x.password);
            db.Employees.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        public bool Delete(int id)
        {
            var data = from s in db.Employees where s.idEmployee == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                db.Employees.DeleteOnSubmit(data.Single());

                db.SubmitChanges();

                return true;
            }
        }

        public Employee Edit(int id)
        {
            var data = from s in db.Employees where s.idEmployee == id select s;

            if (data.Count() == 0)
                return null;
            else
                return data.Single();
        }

        public bool Edit(Employee v)
        {
            var data = from s in db.Employees where s.idEmployee == v.idEmployee select s;

            if (data.Count() == 0)
                return false;
            else
            {
                var e = data.Single();
                 
                e.idTypeE = v.idTypeE;
                e.username = v.username;
                e.name = v.name;
                e.phone = v.phone;
                e.idcard = v.idcard;
                e.address = v.address;
                e.sex = v.sex;
                e.birthday = v.birthday;
                e.startday = v.startday;
                if (v.password != null)
                    e.password = db.md5(v.password);

                db.SubmitChanges();

                return true;
            }
        }
    }
}