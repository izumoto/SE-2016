using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class ScheduleModel
    {
        private DBDataContext db = new DBDataContext();

        /// <summary>
        /// Get all schedule from database
        /// </summary>
        /// <returns></returns>
        public List<Schedule> GetListSchedule()
        {
            var data = from s in db.Schedules select s;
            List<Schedule> list = data.ToList<Schedule>();
            return list;
        }

        public List<Schedule> GetListSchedule(string date)
        {
            List<Schedule> list = null;
            if (date != "")
            {
                /// Format data datetime
                /// dd/mm/yyyy ex: 2/3/2016
                DateTime loadedDate = DateTime.ParseExact(date, "dd/mm/yyyy", null);
                var data = from s in db.Schedules where s.dayStart.Equals(Convert.ToDateTime(date)) select s;
                list = data.ToList<Schedule>();
            }
            else
            {
                var data = from s in db.Schedules select s;
                list = data.ToList<Schedule>();
            }

            return list;
        }

        /// <summary>
        /// Get all time start from database
        /// </summary>
        /// <returns></returns>
        public List<TimeStart> GetListTime()
        {
            var data = from s in db.TimeStarts select s;
            List<TimeStart> list = data.ToList<TimeStart>();
            return list;
        }

        /// <summary>
        /// Add new schedule to database
        /// </summary>
        /// <param name="x">Schedule Object</param>
        public void AddSchedule(Schedule x)
        {
            db.Schedules.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        /// <summary>
        /// Delete schedule by id
        /// </summary>
        /// <param name="id">Id of schedule want to delete</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var data = from s in db.Schedules where s.idSchedule == id select s;
            if (data.Count() == 0)
            {
                return false;
            } 
            else
            {
                db.Schedules.DeleteOnSubmit(data.Single());
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Get schedule by id
        /// </summary>
        /// <param name="id">Id of schedule</param>
        /// <returns></returns>
        public Schedule Edit(int id)
        {
            var data = from s in db.Schedules where s.idSchedule == id select s;
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
        /// Edit schedule's info
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool Edit(Schedule v)
        {
            var data = from s in db.Schedules where s.idSchedule == v.idSchedule select s;
            if (data.Count() == 0)
            {
                return false;
            }  
            else
            {
                /// Add new info
                var e = data.Single();
                e.dayStart = v.dayStart;
                e.dayEnd = v.dayEnd;
                e.idRoute = v.idRoute;
                e.idVehicle = v.idVehicle;
                e.idTime = v.idTime;
                e.price = v.price;
                db.SubmitChanges();
                return true;
            }
        }
    }
}