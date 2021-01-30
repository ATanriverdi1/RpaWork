using RpaWork.Business.Abstract;
using RpaWork.Entities;
using RpaWork.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpaWork.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task Create(Category category)
        {
            await _categoryRepository.Create(category);
        }

        public async Task Delete(Category category)
        {
            await _categoryRepository.Delete(category);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }

        public async Task Update(Category category)
        {
            await _categoryRepository.Update(category);
        }
    }
}
