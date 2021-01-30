using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Abstract
{
   public interface ICartService
    {
        Task<Cart> GetById(string id);
        Task AddToCart(string userId, int productId, int quantity);
        Task DeleteFromCart(string userId, int productId);
        Task CreateCart(string userId);
        Task ClearCart(int cartId);
    }
}
