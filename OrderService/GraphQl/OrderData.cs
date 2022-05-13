using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.GraphQL
{
    public class OrderData
    {
        public string Product { set; get; }
        public int Quantity { set; get; }
        public float Price { set; get; }
    }
}
