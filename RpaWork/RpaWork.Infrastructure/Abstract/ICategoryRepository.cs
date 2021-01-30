using RpaWork.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.Abstract
{
   public interface ICategoryRepository
    {
        List<Category> GetAll();
        Task<Category> GetById(int id);
        Task Create(Category category);
        Task Update(Category category);
        Task Delete(Category category);
    }
}
