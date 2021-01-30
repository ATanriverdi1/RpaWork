using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartManager(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public async Task AddToCart(string userId, int productId, int quantity)
        {
            Cart cart = await GetById(userId);
            if (cart != null)
            {
                List<CartItem> cartItems = cart.CartItems.ToList();
                int index = cartItems.FindIndex(x => x.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cartItems[index].Quantity += quantity;
                }

                await _cartRepository.Update(cart);
            }
        }

        public async Task ClearCart(int cartId)
        {
            await _cartRepository.ClearCart(cartId);
        }

        public async Task CreateCart(string userId)
        {
            await _cartRepository.CreateCart(userId);
        }

        public async Task DeleteFromCart(string userId, int productId)
        {
            var cart = await GetById(userId);
            if (cart != null)
            {
                await _cartRepository.DeleteFromCart(cart.Id, productId);
            }
        }

        public async Task<Cart> GetById(string id)
        {
            return await _cartRepository.GetById(id);
        }
    }
}
