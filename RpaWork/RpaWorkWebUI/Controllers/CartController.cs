using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWork.Identity.Entities;
using RpaWorkWebUI.Models.CartDTOs;
using RpaWorkWebUI.Models.OrderDTOs;

namespace RpaWorkWebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService,
                              IOrderService orderService,
                              UserManager<ApplicationUser> userManager)
        {
            this._cartService = cartService;
            this._orderService = orderService;
            this._userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            Cart cart = await _cartService.GetById(_userManager.GetUserId(User));
            var cartModel = new CartViewModel()
            {
                CartId = cart.Id,
                CartItemViewModels = cart.CartItems.Select(x => new CartItemViewModel()
                {
                    CartItemId = x.Id,
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }).ToList()
            };
            return View(cartModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = _userManager.GetUserId(User);
            await _cartService.AddToCart(userId, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            await _cartService.DeleteFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            string userId = _userManager.GetUserId(User);
            List<Order> orders = _orderService.GetOrders(userId);
            List<OrderModel> orderModels = new List<OrderModel>();
            OrderModel orderModel;
            foreach (var order in orders)
            {
                orderModel = new OrderModel();

                orderModel.Id = order.Id;
                orderModel.Description = order.Description;
                orderModel.OrderDate = order.OrderDate;

                orderModel.OrderDetailModels = order.OrderDetails.Select(i => new OrderDetailModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    Price = (double)i.Price,
                    Quantity = i.Quantity
                }).ToList();
                orderModels.Add(orderModel);
            }
                return View(orderModels);
        }
        
        [HttpGet]
        public IActionResult SaveOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var cart = await _cartService.GetById(userId);

                Order order = new Order();
                order.Id = model.Id;
                order.Description = model.Description;
                order.OrderDate = DateTime.Now;
                order.UserId = userId;
                order.OrderDetails = new List<OrderDetail>(); 
                foreach (var item in cart.CartItems)
                {
                    var orderDetail = new OrderDetail()
                    {
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId
                    };

                    order.OrderDetails.Add(orderDetail);
                }
                
                await _orderService.Create(order);
                await _cartService.ClearCart(cart.Id);
                return RedirectToAction("GetOrders");
            }
            return View();
        }
    }
}
