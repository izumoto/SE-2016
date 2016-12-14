using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SE
{
    public class Notify
    {
        public bool status { get; set; }
        public string msg { get; set; }

        public bool checkError(object o, string type)
        {
            if (o == null)
                return true;
            if (type == "number")
                return !Regex.IsMatch((string)o, @"^\d+$");

            return false;
        }
    }
}