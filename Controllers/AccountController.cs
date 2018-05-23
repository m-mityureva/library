using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl = "/")
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            //return RedirectToAction(nameof(CartController.ClearCart), "Cart", new { returnUrl = returnUrl });

            HttpContext.Session.Clear();
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public ViewResult Register(string returnUrl = "/")
        {
            return View(new RegisterModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await userManager.FindByNameAsync(model.Name) != null)
            {
                ModelState.AddModelError("", "A user with that name already exists");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Name };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Reader");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(BookController.List), "Book");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
