using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Entities
{
   public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
