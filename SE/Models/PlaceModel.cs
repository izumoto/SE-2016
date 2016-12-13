using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class PlaceModel
    {
        private DBDataContext db = new DBDataContext();

        public List<City> GetListCity()
        {
            var data = from s in db.Cities select s;

            List<City> list = data.ToList<City>();

            return list;
        }

        public String GetNameCity(int? id)
        {
            var data = from s in db.Cities where s.idCity == id select s.nameCity;

            return data.SingleOrDefault().ToString();
        }

        public List<Route> GetListRoute()
        {
            var data = from s in db.Routes select s;

            List<Route> list = data.ToList<Route>();

            return list;
        }

        public void AddCity(City x)
        {
            db.Cities.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        public void AddRoute(Route x)
        {
            db.Routes.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        public bool DeleteCity(int id)
        {
            var data = (from s in db.Cities where s.idCity == id select s).Single();

            if (data == null)
                return false;
            else
            {
                db.Cities.DeleteOnSubmit(data);

                db.SubmitChanges();

                return true;
            }
        }

        public bool DeleteRoute(int id)
        {
            var data = (from s in db.Routes where s.idRoute == id select s).Single();

            if (data == null)
                return false;
            else
            {
                db.Routes.DeleteOnSubmit(data);

                db.SubmitChanges();

                return true;
            }
        }

        public City EditCity(int id)
        {
            var data = (from s in db.Cities where s.idCity == id select s).Single();

            return data;
        }

        public bool EditCity(City v)
        {
            var data = (from s in db.Cities where s.idCity == v.idCity select s).Single();

            if (data == null)
                return false;
            else
            {
                data.nameCity = v.nameCity;

                db.SubmitChanges();

                return true;
            }
        }

        public Route EditRoute(int id)
        {
            var data = (from s in db.Routes where s.idRoute == id select s).Single();

            return data;
        }

        public bool EditRoute(Route v)
        {
            var data = (from s in db.Routes where s.idRoute == v.idRoute select s).Single();

            if (data == null)
                return false;
            else
            {
                data.idFrom = v.idFrom;
                data.idTo = v.idTo;

                db.SubmitChanges();

                return true;
            }
        }
    }
}