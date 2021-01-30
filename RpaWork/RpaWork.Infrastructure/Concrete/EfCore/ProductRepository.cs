using Microsoft.EntityFrameworkCore;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using RpaWork.Infrastructure.ApplicationDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.Concrete.EfCore
{
    public class ProductRepository : IProductRepository
    {
        public async Task Create(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.AddAsync(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {   
                return context.Products.Include(x => x.Category).ToList();
            }
        }

        public async Task<Product> GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ApplicationDbContext())
            {
                IQueryable<Product> products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Include(x => x.Category).Where(x => x.Category.Name == category);
                }
                return products.Count();
            }
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName, int page, int pageSize)
        {
            using (var context = new ApplicationDbContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(categoryName))
                {
                    products = products
                                    .Include(i => i.Category).Where(i => i.Category.Name == categoryName);
                }
                return await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task Update(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(product).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
