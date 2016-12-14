using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class VehicleModel
    {
        private DBDataContext db = new DBDataContext();

        /// <summary>
        /// Get all vehicle from database
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> GetListVehicle()
        {
            var data = from s in db.Vehicles select s;
            List<Vehicle> list = data.ToList<Vehicle>();
            return list;
        }

        /// <summary>
        /// Add new vehicle to database
        /// </summary>
        /// <param name="x"></param>
        public void AddVehicle(Vehicle x)
        {
            db.Vehicles.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        /// <summary>
        /// Delete vehicle by id
        /// </summary>
        /// <param name="id">Id vehicle want to delete</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var data = from s in db.Vehicles where s.idVehicle == id select s;
            if (data.Count() == 0)
            {
                return false;
            }   
            else
            {
                db.Vehicles.DeleteOnSubmit(data.Single());
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="id">Id of vehicle</param>
        /// <returns></returns>
        public Vehicle Edit(int id)
        {
            var data = from s in db.Vehicles where s.idVehicle == id select s;
            if (data.Count() == 0)
            {
                return null;
            }
            else
            {
                return data.Single();
            }    
        }

        /// <summary>
        /// Edit vehicle's info
        /// </summary>
        /// <param name="v">Vehicle object</param>
        /// <returns></returns>
        public bool Edit(Vehicle v)
        {
            var data = from s in db.Vehicles where s.idVehicle == v.idVehicle select s;
            if (data.Count() == 0)
            {
                return false;
            }
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