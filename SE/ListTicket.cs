using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SE.Models;

namespace SE
{
    public class ListTicket
    {
        public int idSchedule { get; set; }

        public List<Ticket> ticket { get; set; }
    }
}