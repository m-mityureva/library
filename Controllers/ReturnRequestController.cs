using Library.Infrastructure;
using Library.Models;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class ReturnRequestController : Controller
    {
        IReturnRequestRepository returnRequestRepository;
        IUserBookRepository userBookRepository;
        IBookRepository bookRepository;

        public ReturnRequestController(IReturnRequestRepository returnRequestRepo, IUserBookRepository userBookRepo, IBookRepository bookRepo)
        {
            returnRequestRepository = returnRequestRepo;
            userBookRepository = userBookRepo;
            bookRepository = bookRepo;
        }

        [HttpPost]
        [Authorize(Roles = Roles.Reader)]
        public IActionResult Checkout(int userBookId)
        {
            UserBook userBook = userBookRepository.UserBooks.FirstOrDefault(u => u.Id == userBookId);
            returnRequestRepository.SaveReturnRequest(new ReturnRequest()
            {
                UserBook = userBook,
                Quantity = userBook.Quantity
            });
            return RedirectToAction(nameof(BookController.UserList), "Book");
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ViewResult List() => View(returnRequestRepository.ReturnRequests
                                    .Where(r => !r.Approved)
                                    .Include(r => r.UserBook.Book));


        [Authorize(Roles = Roles.Reader)]
        public ViewResult UserList()
        {
            return View(returnRequestRepository.ReturnRequests
                                .Where(r => !r.Approved && r.UserBook.UserName == User.Identity.Name)
                                .Include(u => u.UserBook.Book));
        }


        [HttpPost]
        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public IActionResult MarkApproved(int returnRequestID)
        {
            ReturnRequest returnRequest = returnRequestRepository.ReturnRequests
                .Include(r => r.UserBook.Book)
                .FirstOrDefault(r => r.Id == returnRequestID);

            returnRequest.UserBook.Book.Available += returnRequest.Quantity;
            bookRepository.SaveBook(returnRequest.UserBook.Book);

            if (returnRequest != null)
            {
                returnRequest.Approved = true;
                returnRequestRepository.SaveReturnRequest(returnRequest);
            }

            return RedirectToAction(nameof(List));
        }
    }
}
