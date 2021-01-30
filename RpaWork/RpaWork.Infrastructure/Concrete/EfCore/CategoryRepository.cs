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
    public class CategoryRepository : ICategoryRepository
    {
        public async Task Create(Category category)
        {
            using (var context = new ApplicationDbContext())
            {
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Category category)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }

        public List<Category> GetAll()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Categories.Include(x=>x.Products).ToList();
            }
        }

        public async Task<Category> GetById(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task Update(Category category)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Entry(category).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
