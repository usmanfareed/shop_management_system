using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DataModel
{
   public class Group
    {
        public int GroupId { get; set; }
        public int PermissionId { get; set; }
        public string GroupName { get; set; }
        public string PermissionName { get; set; }
    }
}
