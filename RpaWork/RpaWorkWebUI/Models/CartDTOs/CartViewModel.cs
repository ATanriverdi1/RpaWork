using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.CartDTOs
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public List<CartItemViewModel> CartItemViewModels { get; set; }

        public double TotalPrice()
        {
            return CartItemViewModels.Sum(i => i.Price * i.Quantity);
        }
    }
}
