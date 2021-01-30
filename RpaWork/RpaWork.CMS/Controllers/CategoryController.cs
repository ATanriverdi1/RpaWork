using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RpaWork.Business.Abstract;
using RpaWork.CMS.Helper.Mapping.Abstract;
using RpaWork.CMS.Models.CategoryDTOs;
using RpaWork.Entities;

namespace RpaWork.CMS.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Kategori")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMapper _categoryMapper;

        public CategoryController(ICategoryService categoryService,
                                  ICategoryMapper categoryMapper)
        {
            this._categoryService = categoryService;
            this._categoryMapper = categoryMapper;
        }
        [Route("KategoriListe")]
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        [HttpGet]
        [Route("YeniKategori")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("YeniKategori")]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = _categoryMapper.MapFromCategoryVM(model);
                await _categoryService.Create(newCategory);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Düzenle")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != 0)
            {
                var category = await _categoryService.GetById(id);
                if (category != null)
                {
                    CategoryViewModel model = _categoryMapper.MapFromCategory(category);
                    return View(model);
                }
                return NotFound();
            }
            return BadRequest(); 
        }

        [HttpPost]
        [Route("Düzenle")]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = await _categoryService.GetById(model.Id);
                if (category != null)
                {
                    category.Id = model.Id;
                    category.Name = model.Name;

                    await _categoryService.Update(category);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View(model);
        }

        [Route("Sil")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            Category category = await _categoryService.GetById(categoryId);
            if (category != null)
            {
                await _categoryService.Delete(category);

                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
