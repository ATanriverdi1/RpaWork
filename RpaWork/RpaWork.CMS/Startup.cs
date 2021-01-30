using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RpaWork.Business.Abstract;
using RpaWork.Business.Concrete;
using RpaWork.CMS.Helper.Mapping.Abstract;
using RpaWork.CMS.Helper.Mapping.Concrete;
using RpaWork.CMS.Models.CategoryDTOs;
using RpaWork.CMS.Models.ProductDTOs;
using RpaWork.CMS.Validators;
using RpaWork.Identity.Data;
using RpaWork.Identity.Entities;
using RpaWork.Infrastructure.Abstract;
using RpaWork.Infrastructure.Concrete.EfCore;

namespace RpaWork.CMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityContext>(options => options
                                                                                .UseSqlServer(@"Data Source=DESKTOP-I338HSF; DataBase = RpaWorkDb; Integrated Security=True"));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityContext>().AddDefaultTokenProviders();

            //Product
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddTransient<IProductMapper, ProductMapper>();
            services.AddTransient<IValidator<ProductViewModel>, ProductValidator>();

            //Category
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddTransient<ICategoryMapper, CategoryMapper>();
            services.AddTransient<IValidator<CategoryViewModel>, CategoryValidator>();

            //IdentitOptions
            services.Configure<IdentityOptions>(options => {

                //Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                //Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                //Default Sign-in settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                //Default User settings.
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                             "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            services.ConfigureApplicationCookie(options =>
            {
                //ReturnUrlParameter requires 
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";

                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                //CookieBuilder
                options.Cookie = new CookieBuilder
                {
                    Name = ".RpaWorkCms.Security.Cookie",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                };
            });

            services.AddControllersWithViews().AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedIdentity.Seed(userManager, roleManager).Wait();
        }
    }
}
