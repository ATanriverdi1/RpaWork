using RpaWorkWebUI.Models.CartDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.OrderDTOs
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public CartViewModel CartViewModel { get; set; }
        public List<OrderDetailModel> OrderDetailModels { get; set; }

        public double TotalPrice()
        {
            return OrderDetailModels.Sum(i => i.Price * i.Quantity);
        }
    }
}
