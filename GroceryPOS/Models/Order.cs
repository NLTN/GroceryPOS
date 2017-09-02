using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryPOS.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public DateTime Datetime { get; set; }

        public OrderDetail[] Details { get; set; }
    }   
}
