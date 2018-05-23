using Library.Infrastructure;
using Library.Models;
using Library.Models.Repositories;
using Library.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private ISession session;
        private IBookRepository bookRepository;
        private IUserBookRepository userBookRepository;
        private IReturnRequestRepository returnRequestRepository;
        public int PageSize = 5;

        public BookController(IServiceProvider services, IBookRepository bookRepo, IUserBookRepository userBookRepo, IReturnRequestRepository returnRequestRepo)
        {
            session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            bookRepository = bookRepo;
            userBookRepository = userBookRepo;
            returnRequestRepository = returnRequestRepo;
        }

        public ViewResult List(int? genreId, int bookPage = 1)
        {
            string search;
            if (HttpContext.Request.Query.ContainsKey("search"))
            {
                search = HttpContext.Request.Query["search"].ToString();
                session.SetString("CurrentSearchString", search);
            }
            else
                search = session.GetString("CurrentSearchString");

            IEnumerable<Book> books = bookRepository.Books
                                .Where(b => genreId == null || b.Genre.Id == genreId)
                                .Where(b => string.IsNullOrEmpty(search)
                                            || b.Title.Contains(search)
                                            || b.Author.Contains(search)
                                            || b.Genre.Title.Contains(search))
                                .OrderBy(b => b.Id);
            return View(new BooksListViewModel
            {
                Books = books.Skip((bookPage - 1) * PageSize)
                                .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = bookPage,
                    ItemsPerPage = PageSize,
                    TotalItems = books.Count()
                },
                CurrentGenreId = genreId,
                CurrentSearchString = search
            });
        }

        [Authorize(Roles = Roles.Reader)]
        public ViewResult UserList()
        {
            return View(userBookRepository.UserBooks
                .Where(b => b.UserName == User.Identity.Name 
                    && b.Quantity > b.Returned 
                    && returnRequestRepository.ReturnRequests.Where(r =>r.UserBook.Id == b.Id).Count() == 0)
                .Include(u => u.Book));
        }

        /*  public static bool CheckAnyFieldContains<T>(T obj, string value)
          {
              var props = typeof(T).GetProperties();
              if (props.Count() == 0)
                  return obj.ToString().Contains(value);

              foreach (PropertyInfo prop in props)
              {
                  var mi = typeof(BookController).GetMethod("CheckAnyFieldContains");
                  var m = mi.MakeGenericMethod(prop.PropertyType);
                  if ((bool)m.Invoke(null, new object[] { prop.GetValue(obj), value }))
                      return true;
              }

              return false;
          }*/
    }
}
