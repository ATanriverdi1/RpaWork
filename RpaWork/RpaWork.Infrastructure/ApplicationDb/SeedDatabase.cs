using Microsoft.EntityFrameworkCore;
using RpaWork.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RpaWork.Infrastructure.ApplicationDb
{
    public static class SeedDatabase
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region CategoryHasData
            Category phone = new Category { Id = 1, Name = "Telefon" };
            Category electronic = new Category { Id = 2, Name = "Elektronik" };
            Category computer = new Category { Id = 3, Name = "Bilgisayar" };

            modelBuilder.Entity<Category>().HasData(new List<Category> { phone, electronic, computer });
            #endregion

            #region ProductHasData
            Product phoneItem = new Product { Id = 1, Name = "iPhone 5s", Price = 2000, CategoryId = phone.Id };
            Product elecItem = new Product { Id = 2, Name = "Samsung QLED", Price = 6500, CategoryId = electronic.Id };
            Product comItem = new Product { Id = 3, Name = "Asus Tuf Gaming", Price = 9500, CategoryId = computer.Id };

            modelBuilder.Entity<Product>().HasData(new List<Product> { phoneItem, elecItem, comItem });
            #endregion
        }
    }
}
