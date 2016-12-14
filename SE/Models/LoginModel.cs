using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        public Employee CheckUser()
        {
            if (UserName == null | Password == null)
                return null;

            DBDataContext db = new DBDataContext();

            var result = db.Employees.SingleOrDefault(x => x.username == UserName);

            if (result != null && result.password == db.md5(Password))
                return result;
            else
                return null;
        }

    }


}