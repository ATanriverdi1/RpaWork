using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RpaWork.Business.Abstract;
using RpaWork.CMS.Helper.Mapping.Abstract;
using RpaWork.CMS.Models.ProductDTOs;
using RpaWork.Entities;

namespace RpaWork.CMS.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Ürün")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductMapper _productMapper;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService,
                                 IProductMapper productMapper,
                                 ICategoryService categoryService)
        {
            this._productService = productService;
            this._productMapper = productMapper;
            this._categoryService = categoryService;
        }
        [Route("ÜrünListesi")]
        public IActionResult Index()
        {
            return View(_productService.GetAll());
        }

        [HttpGet]
        [Route("YeniÜrün")]
        public IActionResult Create()
        {
            List<Category> categories = _categoryService.GetAll();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("YeniÜrün")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = _productMapper.MapFromProductVM(model);
                await _productService.Create(newProduct);

                return RedirectToAction("Index", "Product");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Düzenle")]
        public async Task<IActionResult> Update(int id)
        {
            if (id != 0)
            {
                Product product = await _productService.GetById(id);
                if (product != null)
                {
                    ProductViewModel model = _productMapper.MapFromProduct(product);
                    List<Category> categories = _categoryService.GetAll();
                    ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
                    return View(model);
                }
                return NotFound();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Düzenle")]
        public async Task<IActionResult> Update(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _productService.GetById(model.Id);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.CategoryId = model.CategoryId;

                    await _productService.Update(product);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(model);
        }
        [Route("ÜrünSil")]
        public async Task<IActionResult> Delete(int productId)
        {
            Product product = await _productService.GetById(productId);
            if (product != null)
            {
                await _productService.Delete(product);

                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
