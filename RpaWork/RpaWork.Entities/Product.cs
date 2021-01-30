using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Entities
{
   public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
