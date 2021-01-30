using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task Create(Order order)
        {
            await _orderRepository.Create(order);
        }

        public List<Order> GetOrders(string userId)
        {
            return _orderRepository.GetOrders(userId);
        }
    }
}
