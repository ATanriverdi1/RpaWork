using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task Create(Product product)
        {
            await _productRepository.Create(product);
        }

        public async Task Delete(Product product)
        {
            await _productRepository.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public async Task<Product> GetById(int id)
        {
            return await _productRepository.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productRepository.GetCountByCategory(category);
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName, int page, int pageSize)
        {
            return await _productRepository.GetProductsByCategory(categoryName, page, pageSize);
        }

        public async Task Update(Product product)
        {
            await _productRepository.Update(product);
        }
    }
}
