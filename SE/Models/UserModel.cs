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

        public List<EmployeeType> GetListPos()
        {
            var data = from s in db.EmployeeTypes select s;

            List<EmployeeType> list = data.ToList<EmployeeType>();

            return list;
        }

        public string GetPosition(System.Nullable<int> id)
        {
            var data = from s in db.EmployeeTypes where s.idTypeE == id select s.nameTypeE;

            return data.SingleOrDefault();
        }

        public void AddEmployee(Employee x)
        {
            x.password = db.md5(x.password);
            db.Employees.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        public bool Delete(int id)
        {
            var data = (from s in db.Employees where s.idEmployee == id select s).Single();

            if (data == null)
                return false;
            else
            {
                db.Employees.DeleteOnSubmit(data);

                db.SubmitChanges();

                return true;
            }
        }

        public Employee Edit(int id)
        {
            var data = (from s in db.Employees where s.idEmployee == id select s).Single();

            return data;
        }

        public bool Edit(Employee v)
        {
            var data = (from s in db.Employees where s.idEmployee == v.idEmployee select s).Single();

            if (data == null)
                return false;
            else
            {
                data.idTypeE = v.idTypeE;
                data.username = v.username;
                data.name = v.name;
                data.phone = v.phone;
                data.idcard = v.idcard;
                data.address = v.address;
                data.sex = v.sex;
                data.birthday = v.birthday;
                data.startday = v.startday;

                db.SubmitChanges();

                return true;
            }
        }
    }
}