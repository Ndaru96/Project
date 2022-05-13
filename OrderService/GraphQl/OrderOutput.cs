using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.GraphQL
{
    public class OrderOutput
    {
        public string TransactionDate { set; get; }
        public string Message { set; get; }
    }
}
