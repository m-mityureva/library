using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
        void SaveBook(Book book);
        Book DeleteBook(int bookId);
    }
}
