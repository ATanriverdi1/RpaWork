using RpaWork.CMS.Helper.Mapping.Abstract;
using RpaWork.CMS.Models.CategoryDTOs;
using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWork.CMS.Helper.Mapping.Concrete
{
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryViewModel MapFromCategory(Category category)
        {
            return new CategoryViewModel { Id = category.Id, Name = category.Name, Products = category.Products };
        }

        public Category MapFromCategoryVM(CategoryViewModel category)
        {
            return new Category { Id = category.Id, Name = category.Name, Products = category.Products };
        }
    }
}
