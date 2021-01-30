using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RpaWork.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RpaWork.Identity.Data
{
   public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-I338HSF; DataBase = RpaWorkDb; Integrated Security=True");
        }
    }
}
