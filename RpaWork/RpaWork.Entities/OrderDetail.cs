using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Entities
{
   public class OrderDetail
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
