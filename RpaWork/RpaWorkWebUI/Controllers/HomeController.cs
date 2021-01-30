using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWorkWebUI.Models;
using RpaWorkWebUI.Models.ProductDTOs;

namespace RpaWorkWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService,
                              ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string category, int page = 1)
        {
            const int pageSize = 9;
            ProductListViewModel productListViewModel = new ProductListViewModel()
            {
                BasePage = new BasePage
                {
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    CurrentCategory = category,
                    ItemsPerPage = pageSize
                },
                Products = await _productService.GetProductsByCategory(category, page, pageSize),
                Categories = _categoryService.GetAll()
            };
            return View(productListViewModel);
        }
    }
}
