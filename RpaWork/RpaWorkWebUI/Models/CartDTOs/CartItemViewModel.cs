using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.CartDTOs
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
