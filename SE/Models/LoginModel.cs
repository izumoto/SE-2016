using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SE.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        /// <summary>
        /// Check user is exit
        /// </summary>
        /// <returns></returns>
        public Employee CheckUser()
        {
            if (UserName == null | Password == null)
                return null;
            DBDataContext db = new DBDataContext();
            /// Get user with username and password
            var result = db.Employees.SingleOrDefault(x => x.username == UserName);
            if (result != null && result.password == md5(Password))
            {
                return result;
            }
            else
            {
                return null;
            }   
        }

        /// <summary>
        /// Get md5 code
        /// </summary>
        /// <returns></returns>
        public static string md5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }


}