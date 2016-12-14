using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class HomeModel
    {
        private DBDataContext db = new DBDataContext();

        /// <summary>
        /// Get count of tickets
        /// </summary>
        /// <returns></returns>
        public int CountOrders()
        {
            var data = from s in db.Tickets select s;
            return data.Count();
        }

        /// <summary>
        /// Get count of routes
        /// </summary>
        /// <returns></returns>
        public int CountRoutes()
        {
            var data = from s in db.Routes select s;
            return data.Count();
        }

        /// <summary>
        /// Get count of schediles
        /// </summary>
        /// <returns></returns>
        public int CountSchedules()
        {
            var data = from s in db.Schedules select s;
            return data.Count();
        }

        /// <summary>
        /// Get count of sales
        /// </summary>
        /// <returns></returns>
        public int CountSales()
        {
            var data = from s in db.Tickets join v in db.Schedules on s.idSchedule equals v.idSchedule select v.price;
            int result = 0;
            foreach (int e in data)
            {
                result += e;
            }

            return result;
        }
    }
}