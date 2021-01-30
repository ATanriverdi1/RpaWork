using Microsoft.EntityFrameworkCore;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using RpaWork.Infrastructure.ApplicationDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.Concrete.EfCore
{
    public class OrderRepository : IOrderRepository
    {
        public async Task Create(Order order)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }

        public List<Order> GetOrders(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = context.Orders.Include(x=>x.OrderDetails).ThenInclude(x=>x.Product).AsQueryable();
                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId).OrderByDescending(i => i.Id);
                }

                return orders.ToList();
            }
        }
    }
}
