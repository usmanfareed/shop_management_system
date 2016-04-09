using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public static class BaseClass
    {
        public static int UserId { get; set; }
        public static string Connection
        {
            get { return ConfigurationManager.ConnectionStrings["SMS_DB_Connection"].ConnectionString.ToString(); }
        }
    }
}
