using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class VehicleModel
    {
        private DBDataContext db = new DBDataContext();

        public List<Vehicle> GetListVehicle()
        {
            var data = from s in db.Vehicles select s;

            List<Vehicle> list = data.ToList<Vehicle>();

            return list;
        }

        public void AddVehicle(Vehicle x)
        {
            db.Vehicles.InsertOnSubmit(x);
            db.SubmitChanges();
            
        }

        public bool Delete(int id)
        {
            var data = (from s in db.Vehicles where s.idVehicle == id select s).Single();

            if (data == null)
                return false;
            else
            {
                db.Vehicles.DeleteOnSubmit(data);

                db.SubmitChanges();

                return true;
            }
        }

        public Vehicle Edit(int id)
        {
            var data = (from s in db.Vehicles where s.idVehicle == id select s).Single();

            return data;
        }

        public bool Edit(Vehicle v)
        {
            var data = (from s in db.Vehicles where s.idVehicle == v.idVehicle select s).Single();

            if (data == null)
                return false;
            else
            {
                data.license = v.license;
                data.dayImport = v.dayImport;

                db.SubmitChanges();

                return true;
            }
        }
    }
}