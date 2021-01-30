﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Entities
{
   public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
