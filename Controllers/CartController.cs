using Library.Infrastructure;
using Library.Models;
using Library.Models.Repositories;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository repository;
        private Cart cart;

        public CartController(IBookRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {

            string modelStateString = TempData["ModelState"]?.ToString() ?? "";
            if (!string.IsNullOrEmpty(modelStateString))
            {
                var listError = JsonConvert.DeserializeObject<Dictionary<string, string>>(modelStateString);
                var modelState = new ModelStateDictionary();
                foreach (var item in listError)
                {
                    modelState.AddModelError(item.Key, item.Value ?? "");
                }

                ViewData.ModelState.Merge(modelState);
            }
            
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int bookId, string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                cart.AddItem(book, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int bookId, string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == bookId);

            if (book != null)
            {
                cart.RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectResult ClearCart(string returnUrl)
        {
            cart.Clear();
            return Redirect(returnUrl);
        }

    }
}
