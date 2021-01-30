using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.Abstract
{
   public interface ICartRepository
    {
        Task<Cart> GetById(string id);
        Task Update(Cart cart);
        List<Cart> GetAll();
        Task DeleteFromCart(int cartId, int productId);
        Task CreateCart(string userId);
        Task ClearCart(int cartId);
    }
}
