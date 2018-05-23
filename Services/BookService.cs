using Library.Models;
using Library.Models.Repositories;
using Library.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetList(int? genreId, int bookPage = 1, string search = null)
        {
            /*return bookRepository.Books
                .Where(b => genreId == null || b.Genre.Id == genreId)
                .Where(b => string.IsNullOrEmpty(search) || AnyFieldContains(b, search))
                .OrderBy(b => b.Id)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize).ToList();*/
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            /*return bookRepository.Books
                .Count(b => (genreId == null || b.Genre.Id == genreId)
                && string.IsNullOrEmpty(search) || AnyFieldContains(b, search)
                );*/

            throw new NotImplementedException();
        }



        private bool AnyFieldContains(Book book, string search) => book.Title.Contains(search)
                                                                || book.Author.Contains(search)
                                                                || book.Genre.Title.Contains(search);
    }
}
