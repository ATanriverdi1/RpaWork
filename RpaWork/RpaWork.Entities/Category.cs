﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Entities
{
   public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
