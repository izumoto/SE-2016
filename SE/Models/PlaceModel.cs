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
            var data = from s in db.Cities where s.idCity == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                db.Cities.DeleteOnSubmit(data.Single());

                db.SubmitChanges();

                return true;
            }
        }

        public bool DeleteRoute(int id)
        {
            var data = from s in db.Routes where s.idRoute == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                db.Routes.DeleteOnSubmit(data.Single());

                db.SubmitChanges();

                return true;
            }
        }

        public City EditCity(int id)
        {
            var data = from s in db.Cities where s.idCity == id select s;

            if (data.Count() == 0)
                return null;
            else
                return data.Single();
        }

        public bool EditCity(City v)
        {
            var data = from s in db.Cities where s.idCity == v.idCity select s;

            if (data.Count() == 0)
                return false;
            else
            {
                var e = data.Single();

                e.nameCity = v.nameCity;

                db.SubmitChanges();

                return true;
            }
        }

        public Route EditRoute(int id)
        {
            var data = from s in db.Routes where s.idRoute == id select s;

            if (data.Count() == 0)
                return null;
            else
                return data.Single();
        }

        public bool EditRoute(Route v)
        {
            var data = from s in db.Routes where s.idRoute == v.idRoute select s;

            if (data.Count() == 0)
                return false;
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