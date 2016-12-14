using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class TicketModel
    {
        private DBDataContext db = new DBDataContext();

        /// <summary>
        /// Get all ticket by id of schedule
        /// </summary>
        /// <param name="schedule">Id of schedule</param>
        /// <returns></returns>
        public List<Ticket> GetListTicket(int schedule)
        {
            var data = from s in db.Tickets where s.idSchedule == schedule select s;
            List<Ticket> list = data.ToList<Ticket>();
            return list;
        }

        /// <summary>
        /// Get all status pay 
        /// 1: paid
        /// 2: unpaid
        /// </summary>
        /// <returns></returns>
        public List<StatusPay> GetListStatus()
        {
            var data = from s in db.StatusPays select s;
            List<StatusPay> list = data.ToList<StatusPay>();
            return list;
        }

        /// <summary>
        /// Add new ticket to database
        /// </summary>
        /// <param name="x">Ticket Object</param>
        public void AddTicket(Ticket x)
        {
            db.Tickets.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        /// <summary>
        /// Delete ticket by id
        /// </summary>
        /// <param name="id">Id of ticket want to delete</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var data = from s in db.Tickets where s.idTicket == id select s;
            if (data.Count() == 0)
            {
                return false;
            }  
            else
            {
                db.Tickets.DeleteOnSubmit(data.Single());
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Paid for ticket by id
        /// </summary>
        /// <param name="id">Id of ticket</param>
        /// <returns></returns>
        public bool Paid(int id)
        {
            var data = from s in db.Tickets where s.idTicket == id select s;
            if (data.Count() == 0)
            {
                return false;
            }  
            else
            {
                /// Set new status pay for ticket
                /// with idstatus = 1 : paid
                var e = data.Single();
                e.idStatus = 1;
                db.SubmitChanges();
                return true;
            }
        }
    }
}