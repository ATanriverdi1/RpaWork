using RpaWork.CMS.Helper.Mapping.Abstract;
using RpaWork.CMS.Models.ProductDTOs;
using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWork.CMS.Helper.Mapping.Concrete
{
    public class ProductMapper : IProductMapper
    {
        public ProductViewModel MapFromProduct(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }

        public Product MapFromProductVM(ProductViewModel product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }
    }
}
