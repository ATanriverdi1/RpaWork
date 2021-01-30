using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RpaWork.Business.Abstract;
using RpaWork.Identity.Entities;
using RpaWorkWebUI.Models.IdentityDTOs;

namespace RpaWorkWebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICartService _cartService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ICartService cartService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._cartService = cartService;
        }


        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        await _cartService.CreateCart(user.Id);
                        return Redirect(model.ReturnUrl ?? "~/");
                    }
                    ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
                    return View(model);
                }
                ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
