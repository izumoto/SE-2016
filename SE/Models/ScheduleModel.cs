using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class ScheduleModel
    {
        private DBDataContext db = new DBDataContext();

        public List<Schedule> GetListSchedule()
        {
            var data = from s in db.Schedules select s;

            List<Schedule> list = data.ToList<Schedule>();

            return list;
        }

        public void AddSchedule(Schedule x)
        {
            db.Schedules.InsertOnSubmit(x);
            db.SubmitChanges();

        }

        public bool Delete(int id)
        {
            var data = (from s in db.Schedules where s.idSchedule == id select s).Single();

            if (data == null)
                return false;
            else
            {
                db.Schedules.DeleteOnSubmit(data);

                db.SubmitChanges();

                return true;
            }
        }

        public Schedule Edit(int id)
        {
            var data = (from s in db.Schedules where s.idSchedule == id select s).Single();

            return data;
        }

        public bool Edit(Schedule v)
        {
            var data = (from s in db.Schedules where s.idSchedule == v.idSchedule select s).Single();

            if (data == null)
                return false;
            else
            {
                data.dayStart = v.dayStart;
                data.dayEnd = v.dayEnd;
                data.idRoute = v.idRoute;

                db.SubmitChanges();

                return true;
            }
        }
    }
}