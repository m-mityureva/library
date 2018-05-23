using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetList(int? genreId, int bookPage = 1, string search = null);
        int GetTotalCount();
    }
}
