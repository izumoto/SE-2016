using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class TicketModel
    {
        private DBDataContext db = new DBDataContext();

        public List<Ticket> GetListTicket(int schedule)
        {
            var data = from s in db.Tickets where s.idSchedule == schedule select s;

            List<Ticket> list = data.ToList<Ticket>();

            return list;
        }

        public List<StatusPay> GetListStatus()
        {
            var data = from s in db.StatusPays select s;

            List<StatusPay> list = data.ToList<StatusPay>();

            return list;
        }

        public void AddTicket(Ticket x)
        {
            db.Tickets.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        public bool Delete(int id)
        {
            var data = from s in db.Tickets where s.idTicket == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                db.Tickets.DeleteOnSubmit(data.Single());

                db.SubmitChanges();

                return true;
            }
        }

        public bool Paid(int id)
        {
            var data = from s in db.Tickets where s.idTicket == id select s;

            if (data.Count() == 0)
                return false;
            else
            {
                var e = data.Single();

                e.idStatus = 1;

                db.SubmitChanges();

                return true;
            }
        }
    }
}