using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.ProductDTOs
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
