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
    public class CartRepository : ICartRepository
    {
        public async Task ClearCart(int cartId)
        {
            using (var context = new ApplicationDbContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0";
                await context.Database.ExecuteSqlRawAsync(cmd, cartId);
            }
        }

        public async Task CreateCart(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var carts = await context.Carts.ToListAsync();
                if (carts.Count == 0)
                {
                    Cart newCart = new Cart { UserId = userId };
                    await context.Carts.AddAsync(newCart);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ApplicationDbContext())
            {
                string cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
                await context.Database.ExecuteSqlRawAsync(cmd, cartId, productId);
            }
        }

        public List<Cart> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Carts.ToList();
            }
        }

        public async Task<Cart> GetById(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == id);
            }
        }

        public async Task Update(Cart cart)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Carts.Update(cart);
                await context.SaveChangesAsync();
            }
        }
    }
}
