using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models.ProductDTOs
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public BasePage BasePage { get; set; }
        public List<Category> Categories { get; set; }
    }
}
