using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.OrderDTOs
{
    public class OrderDetailModel
    {
        public int OrderItemId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
