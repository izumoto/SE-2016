using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class PlaceModel
    {
        private DBDataContext db = new DBDataContext();

        /// <summary>
        /// Get all city from database
        /// </summary>
        /// <returns></returns>
        public List<City> GetListCity()
        {
            var data = from s in db.Cities select s;
            List<City> list = data.ToList<City>();
            return list;
        }

        /// <summary>
        /// Get all route from database
        /// </summary>
        /// <returns></returns>
        public List<Route> GetListRoute()
        {
            var data = from s in db.Routes select s;
            List<Route> list = data.ToList<Route>();
            return list;
        }

        /// <summary>
        /// Add new city
        /// </summary>
        /// <param name="x">City object</param>
        public void AddCity(City x)
        {
            db.Cities.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        /// <summary>
        /// Add new rount
        /// </summary>
        /// <param name="x">Rount object</param>
        public void AddRoute(Route x)
        {
            db.Routes.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        /// <summary>
        /// Delete city by id
        /// </summary>
        /// <param name="id">Id of city want to delete</param>
        /// <returns></returns>
        public bool DeleteCity(int id)
        {
            var data = from s in db.Cities where s.idCity == id select s;
            if (data.Count() == 0)
            {
                return false;
            }
            else
            {
                db.Cities.DeleteOnSubmit(data.Single());
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Delete route by id
        /// </summary>
        /// <param name="id">Id of route want to delete</param>
        /// <returns></returns>
        public bool DeleteRoute(int id)
        {
            var data = from s in db.Routes where s.idRoute == id select s;
            if (data.Count() == 0)
            {
                return false;
            }    
            else
            {
                db.Routes.DeleteOnSubmit(data.Single());
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Get City by id
        /// </summary>
        /// <param name="id">Id of city</param>
        /// <returns></returns>
        public City EditCity(int id)
        {
            var data = from s in db.Cities where s.idCity == id select s;
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
        /// Edit city's info
        /// </summary>
        /// <param name="v">City object</param>
        /// <returns></returns>
        public bool EditCity(City v)
        {
            var data = from s in db.Cities where s.idCity == v.idCity select s;
            if (data.Count() == 0)
            {
                return false;
            }    
            else
            {
                var e = data.Single();
                e.nameCity = v.nameCity;
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Get route by id
        /// </summary>
        /// <param name="id">Id of Route</param>
        /// <returns></returns>
        public Route EditRoute(int id)
        {
            var data = from s in db.Routes where s.idRoute == id select s;
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
        /// Edit route's info
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool EditRoute(Route v)
        {
            var data = from s in db.Routes where s.idRoute == v.idRoute select s;
            if (data.Count() == 0)
            {
                return false;
            }  
            else
            {
                var e = data.Single();
                e.idFrom = v.idFrom;
                e.idTo = v.idTo;
                db.SubmitChanges();
                return true;
            }
        }
    }
}