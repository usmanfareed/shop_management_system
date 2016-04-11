using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DataModel
{
    public class User:Group
    {

        public int  UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }


    }
}
