using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.DataModel
{
 
   public class Product
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public string  Description { get; set; }
        public DateTime AddedDate { get; set; }
        public string Addedby { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public byte IsAvailable { get; set; }
    }
}
