using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class CustomerModel
    {
        private DBDataContext db = new DBDataContext();
        
        /// <summary>
        /// Get list of customer from database
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetListCustomer()
        {
            var data = from s in db.Customers select s;
            List<Customer> list = data.ToList<Customer>();
            return list;
        }

        /// <summary>
        /// Add new customer to database
        /// </summary>
        /// <param name="x"></param>
        public void AddCustomer(Customer x)
        {
            db.Customers.InsertOnSubmit(x);
            db.SubmitChanges();
        }
    }
}