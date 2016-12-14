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
            var data = from s in db.Vehicles where s.idVehicle == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                db.Vehicles.DeleteOnSubmit(data.Single());

                db.SubmitChanges();

                return true;
            }
        }

        public Vehicle Edit(int id)
        {
            var data = from s in db.Vehicles where s.idVehicle == id select s;

            if (data.Count() == 0)
                return null;
            else
                return data.Single();
        }

        public bool Edit(Vehicle v)
        {
            var data = from s in db.Vehicles where s.idVehicle == v.idVehicle select s;

            if (data.Count() == 0)
                return false;
            else
            {
                var e = data.Single();

                e.license = v.license;
                e.dayImport = v.dayImport;

                db.SubmitChanges();

                return true;
            }
        }
    }
}