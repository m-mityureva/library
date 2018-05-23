using Library.Infrastructure;
using Library.Models;
using Library.Models.Repositories;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
    public class AdminController : Controller
    {
        private IBookRepository bookRepository;
        private IGenreRepository genreRepository;
        public int PageSize = 5;

        public AdminController(IBookRepository bookRepo, IGenreRepository genreRepo)
        {
            bookRepository = bookRepo;
            genreRepository = genreRepo;
        }

        public ViewResult Index(int bookPage = 1)
        {
            ViewBag.AdminPageInfo = new PagingInfo
            {
                CurrentPage = bookPage,
                ItemsPerPage = PageSize,
                TotalItems = bookRepository.Books.Count()
            };
            return View(bookRepository.Books
                                      .Skip((bookPage - 1) * PageSize)
                                      .Take(PageSize).ToList());
        }

        public ViewResult Edit(int bookId) => View(new BookEditViewModel
        {
            Genres = genreRepository.Genres,
            Book = bookRepository.Books.FirstOrDefault(b => b.Id == bookId)
        });

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Genre = genreRepository.Genres.FirstOrDefault(g => g.Id == book.Genre.Id);
                bookRepository.SaveBook(book);
                TempData["message"] = $"{book.Title} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(book);
            }
        }

        public ViewResult Create() => View("Edit", new BookEditViewModel
        {
            Genres = genreRepository.Genres,
            Book = new Book()
        });

        [HttpPost]
        public IActionResult Delete(int bookId)
        {
            Book deletedBook = bookRepository.DeleteBook(bookId);
            if (deletedBook != null)
            {
                TempData["message"] = $"{deletedBook.Title} was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
