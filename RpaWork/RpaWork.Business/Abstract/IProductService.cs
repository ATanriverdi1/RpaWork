using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Abstract
{
   public interface IProductService
    {
        List<Product> GetAll();
        Task<Product> GetById(int id);
        Task Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        int GetCountByCategory(string category);
        Task<List<Product>> GetProductsByCategory(string categoryName, int page, int pageSize);
    }
}
