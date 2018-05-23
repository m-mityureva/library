using Library.Infrastructure;
using Library.Models;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepository;
        private IUserBookRepository userBookRepository;
        private IBookRepository bookRepository;
        private Cart cart;
        private string currentUser;

        public OrderController(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepoService,
            IUserBookRepository userBookRepoService, IBookRepository bookRepoService, Cart cartService)
        {
            orderRepository = orderRepoService;
            userBookRepository = userBookRepoService;
            bookRepository = bookRepoService;
            cart = cartService;
            currentUser = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ViewResult List() => View(orderRepository.Orders.Where(o => !o.Approved));

        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public IActionResult MarkApproved(int orderID)
        {
            Order order = orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderID);
            if (order != null)
            {
                order.Approved = true;
                orderRepository.SaveOrder(order);
            }

            foreach (var line in order.Lines)
            {
                UserBook userBook = new UserBook
                {
                    UserName = order.UserName,
                    Book = line.Book,
                    Quantity = line.Quantity,
                    IssuanceDate = DateTime.Now
                };
                userBookRepository.SaveUserBook(userBook);
                line.Book.Available -= line.Quantity;
                bookRepository.SaveBook(line.Book);
            }

            return RedirectToAction(nameof(List));
        }

        public IActionResult Checkout(string returnUrl)
        {
            foreach (CartLine line in cart.Lines)
            {
                if (line.Book.Available == 0)
                    ModelState.AddModelError("", $"\"{line.Book.Title}\" is not available");
                else
                if (line.Quantity > line.Book.Available)
                    ModelState.AddModelError("", $"Please enter a quantity of \"{line.Book.Title}\" from 1 to {line.Book.Available}");
            }
            if (ModelState.IsValid)
                return View(new Order());
            else
            {
                if (ModelState != null)
                {
                    var listError = ModelState.Where(x => x.Value.Errors.Any())
                        .ToDictionary(m => m.Key, m => m.Value.Errors
                        .Select(s => s.ErrorMessage)
                        .FirstOrDefault(s => s != null));
                    TempData["ModelState"] = JsonConvert.SerializeObject(listError);
                }

                return RedirectToAction(nameof(CartController.Index), "Cart", new { returnUrl = returnUrl });
            }

        }

        [HttpPost]
        [Authorize(Roles = Roles.Reader)]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                order.UserName = currentUser;
                orderRepository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
