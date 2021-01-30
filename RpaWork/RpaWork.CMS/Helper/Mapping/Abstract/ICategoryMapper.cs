using RpaWork.CMS.Models.CategoryDTOs;
using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWork.CMS.Helper.Mapping.Abstract
{
   public interface ICategoryMapper
    {
        Category MapFromCategoryVM(CategoryViewModel category);
        CategoryViewModel MapFromCategory(Category category);
    }
}
