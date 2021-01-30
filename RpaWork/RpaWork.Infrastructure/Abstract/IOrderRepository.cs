using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.Abstract
{
   public interface IOrderRepository
    {
        List<Order> GetOrders(string userId);
        Task Create(Order order);
    }
}
