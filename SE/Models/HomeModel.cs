using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class HomeModel
    {
        private DBDataContext db = new DBDataContext();

        public int CountOrders()
        {
            var data = from s in db.Tickets select s;

            return data.Count();
        }

        public int CountRoutes()
        {
            var data = from s in db.Routes select s;

            return data.Count();
        }

        public int CountSchedules()
        {
            var data = from s in db.Schedules select s;

            return data.Count();
        }

        public int CountSales()
        {
            var data = (from s in db.Tickets join v in db.Schedules on s.idSchedule equals v.idSchedule select v.price);

            int result = 0;

            foreach (int e in data)
            {
                result += e;
            }

            return result;
        }
    }
}